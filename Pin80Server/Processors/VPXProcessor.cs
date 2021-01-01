using Pin80Server.Models.JSONSerializer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

namespace Pin80Server.CommandProcessors
{
    public class VPXProcessor : Processor
    {
        private const int LagIgnoreMS = 30;

        // Store tasks by target
        private readonly Dictionary<string, List<ProcessorTask>> targetTasks = new Dictionary<string, List<ProcessorTask>>();

        public VPXProcessor(DataProcessor d, SerialPort s) : base(d, s)
        {
        }

        public static (bool, string, string, string) splitCommandString(string command)
        {
            string[] commandParts = command.Split(' ');
            if (commandParts.Length < 3)
            {
                Debug.WriteLine(string.Format("Command is not valid: {0}", command));
                return (false, null, null, null);
            }
            string triggerString = commandParts[0];
            string valueString = commandParts[1];
            string extraString = string.Join(" ", commandParts.Skip(2));

            return (true, triggerString, valueString, extraString);
        }

        /*
         * Commands are broken down as:
         */
        public override bool processCommand(string command)
        {
            if (serial == null)
            {
                return false;
            }

            try
            {
                var (success, triggerString, valueString, extraString) = splitCommandString(command);
                if (!success)
                {
                    mainForm.addLogEntry(string.Format("ERR command is not valid: {0}", command));
                    return false;
                }

                var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                /* Table information */
                if (triggerString == "INFO")
                {
                    if (valueString == "ROM")
                    {
                        _romName = extraString;
                        if (mainForm.IsHandleCreated)
                        {
                            mainForm.BeginInvoke((MethodInvoker)delegate ()
                            {
                                mainForm.setRomName(_romName);
                            });
                        }
                        return true;
                    }
                    return false;
                }

                var sentMS = Convert.ToInt64(extraString);
                var lag = now - sentMS;

                if (lag > LagIgnoreMS)
                {
                    if (mainForm.IsHandleCreated)
                    {
                        mainForm.BeginInvoke((MethodInvoker)delegate ()
                        {
                            mainForm.addLogEntry(string.Format("ERR Ignoring command {0} as it's too old: {1}", command, lag));
                        });
                    }
                }

                var items = dataProcessor.getControlItems(triggerString);

                /* If we didn't match any items see if we should add this to the list */
                if (items.Count == 0 && dataProcessor.autoAddItems)
                {
                    if (mainForm.IsHandleCreated)
                    {
                        mainForm.BeginInvoke((MethodInvoker)delegate ()
                        {
                            ControlItem item = new ControlItem(triggerString, valueString);
                            dataProcessor.addControlItem(item);
                        });
                    }
                }

                /* Pretection so we don't run items with the same target */
                HashSet<string> processedTargets = new HashSet<string>();
                foreach (var item in items)
                {
                    if (!item.enabled)
                    {
                        continue;
                    }

                    var target = dataProcessor.getTarget(item.targetString);
                    var trigger = dataProcessor.getTrigger(item.triggerString);
                    var effect = dataProcessor.getEffect(item.effectString);

                    if (effect == null || trigger == null || target == null)
                    {
                        throw new Exception("Could not handle effect");
                    }

                    var stopDuplicateKey = string.Format("{0}{1}", target.id, effect.delay);

                    if (processedTargets.Contains(stopDuplicateKey))
                    {
                        Debug.WriteLine("Already processed an item for this target!");
                        Debug.WriteLine(stopDuplicateKey);
                        continue;
                    }

                    // TODO Check serial connection is all set still.
                    if (effect.Validate(valueString, item))
                    {
                        effect.Handle(target);
                        processedTargets.Add(stopDuplicateKey);
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(string.Format("Could not process command: {0} {1}", command, e));
                mainForm.addLogEntry(string.Format("ERR failed to process: {0}", command));
                return false;
            }
        }
    }
}
