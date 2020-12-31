using Pin80Server.Models.JSONSerializer;
using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

namespace Pin80Server.CommandProcessors
{
    internal class VPXProcessor : IProcessor
    {
        private readonly SerialPort serial;
        private readonly DataProcessor dataProcessor;
        private MainForm mainForm;

        private string _romName;
        private const int LagIgnoreMS = 30;

        public VPXProcessor(DataProcessor d, SerialPort s)
        {
            serial = s;
            dataProcessor = d;
        }
        public string romName()
        {
            return _romName;
        }
        public void setMainForm(MainForm mf)
        {
            mainForm = mf;
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
         * TRIGGER ACTION TIMESTAMP
         */
        public bool processCommand(string command)
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

                foreach (var item in items)
                {
                    if (!item.enabled)
                    {
                        continue;
                    }
                    var target = dataProcessor.getTarget(item.targetString);
                    var trigger = dataProcessor.getTrigger(item.triggerString);
                    var action = dataProcessor.getAction(item.actionString);

                    if (action == null || trigger == null || target == null)
                    {
                        throw new Exception("Could not handle action");
                    }

                    // TODO Check serial connection is all set still.
                    if (action.Validate(valueString, item))
                    {
                        action.Handle(valueString, item, trigger, target, serial);
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
