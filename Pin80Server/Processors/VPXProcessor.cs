using Pin80Server.Models.JSONSerializer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pin80Server.CommandProcessors
{
    public class VPXProcessor : Processor
    {
        private const int LagIgnoreMS = 30;

        // Store tasks by target
        private Dictionary<string, List<ProcessorTask>> targetTasks = new Dictionary<string, List<ProcessorTask>>();

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
         * TRIGGER ACTION TIMESTAMP
         */
        override public bool processCommand(string command)
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
                    var action = dataProcessor.getAction(item.actionString);

                    if (action == null || trigger == null || target == null)
                    {
                        throw new Exception("Could not handle action");
                    }

                    if (processedTargets.Contains(target.id))
                    {
                        Debug.WriteLine("Already processed an item for this target!");
                        continue;
                    }

                    // TODO Check serial connection is all set still.
                    if (action.Validate(valueString, item))
                    {
                        // First check if this target is already executing tasks
                        if (targetTasks.ContainsKey(target.id) && targetTasks[target.id].Count > 0)
                        {
                            var runningTasks = targetTasks[target.id];
                            foreach (var pt in runningTasks)
                            {
                                if (!pt.task.IsCompleted)
                                {
                                    Debug.WriteLine("Task is still running, issuing stop");
                                    pt.token.Cancel();
                                }
                            }
                            runningTasks.Clear();
                        }

                        Debug.WriteLine("Process task.");
                        var processorTask = action.Handle(valueString, item, trigger, target, serial);

                        processedTargets.Add(target.id);

                        if (targetTasks.ContainsKey(target.id))
                        {
                            Debug.WriteLine("Key exists, adding.");
                            targetTasks[target.id].Add(processorTask);
                        }
                        else
                        {
                            targetTasks[target.id] = new List<ProcessorTask>();
                            targetTasks[target.id].Add(processorTask);
                        }
                    }
                }
                //var task = Task.Run(async delegate
                //{
                //    if (targetTasks.ContainsKey(Target.id))
                //    var allTasks = targetTasks[target.id];
                //    await Task.WhenAll(tasks.Select(t => t.task).ToArray());
                //    Debug.WriteLine("All tasks are done");
                //});

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
