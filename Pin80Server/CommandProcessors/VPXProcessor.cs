using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;

namespace Pin80Server.CommandProcessors
{
    class VPXProcessor : Processor
    {
        private SerialPort serial;
        private string _romName;

        public VPXProcessor(SerialPort s)
        {
            serial = s;
        }
        public string romName()
        {
            return _romName;
        }

        /*
         * Commands are broken down as:
         * TRIGGER ACTION TIMESTAMP
         */
        public bool processCommand(string command, Action<Processor> callback)
        {
            if (serial == null)
            {
                return false;
            }

            string[] commandParts = command.Split(' ');
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
                    callback(this);
                    return true;
                }
            }


            if (!trigger.StartsWith("L")) //Lamps
            {
                return true;
            }

            //Debug.WriteLine("Process... " + command);
            try
            {
                var sentMS = Convert.ToInt64(extra);
                var lag = now - sentMS;

                if (commandParts[0] == "S11" || commandParts[0] == "S13" || commandParts[0] == "S12" || commandParts[0] == "S48" || commandParts[0] == "S46")
                {
                    // TODO FIX THIS
                    serial.Write(string.Format("{0} {1}\n", "S48", commandParts[1]));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(string.Format("Could not process command: {0} {1}", command, e));
                return false;
            }

            return true;
        }
    }
}
