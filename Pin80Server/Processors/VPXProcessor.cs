using Pin80Server.Models.JSONSerializer;
using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Pin80Server.CommandProcessors
{
    internal class VPXProcessor : Processor
    {
        private readonly SerialPort serial;
        private readonly DataProcessor dataProcessor;

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

        /*
         * Commands are broken down as:
         * TRIGGER ACTION TIMESTAMP
         */
        public bool processCommand(string command, MainForm mainForm)
        {
            if (serial == null)
            {
                return false;
            }

            try
            {
                string[] commandParts = command.Split(' ');

                if (commandParts.Length < 3)
                {
                    Debug.WriteLine(string.Format("Command is not valid: {0}", command));
                    return false;
                }

                string trigger = commandParts[0];
                string value = commandParts[1];
                string extra = string.Join(" ", commandParts.Skip(2));

                var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                /* Table information */
                if (trigger == "INFO")
                {
                    if (value == "ROM")
                    {
                        _romName = extra;
                        //callback(this);
                        if (mainForm.IsHandleCreated)
                        {
                            mainForm.BeginInvoke((MethodInvoker)delegate ()
                            {
                                mainForm.setRomName(_romName);
                            });
                        }
                        dataProcessor.LoadTableInformation(_romName, mainForm);
                        return true;
                    }

                    return false;
                }

                var sentMS = Convert.ToInt64(extra);
                var lag = now - sentMS;

                if (lag > LagIgnoreMS)
                {
                    // TODO log this to the UI
                    Debug.WriteLine(string.Format("Ignoring command {0} as it's too old: {1}", command, lag));
                }

                var items = dataProcessor.getControlItems(trigger);

                // TODO enable this mode, it's probably slow
                if (items.Count == 0)
                {
                    if (mainForm.IsHandleCreated)
                    {
                        mainForm.BeginInvoke((MethodInvoker)delegate ()
                        {
                            ControlItem item = new ControlItem(trigger, value);
                            dataProcessor.addControlItem(item);
                        });
                    }
                }
                Debug.WriteLine("Process " + items.Count);

                foreach(var item in items)
                {
                    if (!item.enabled)
                    {
                        continue;
                    }
                    var target = dataProcessor.getTarget(item.targetString);
                    var trig = dataProcessor.getTrigger(item.triggerString);
                    var action = dataProcessor.getAction(item.actionString);

                    if (action != null)
                    {
                        action.handle(value, item, trig, target, serial);
                    }
                    else
                    {
                        //throw new Exception("Could not handle action");
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(string.Format("Could not process command: {0} {1}", command, e));
                return false;
            }
        }
    }
}
