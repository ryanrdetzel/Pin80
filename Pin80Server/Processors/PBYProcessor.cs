using System;
using System.IO.Ports;

namespace Pin80Server.CommandProcessors
{
    internal class PBYProcessor : Processor
    {
        private readonly SerialPort serial;
        private string _romName;

        public PBYProcessor(SerialPort s)
        {
            serial = s;
        }

        public string romName()
        {
            return _romName;
        }

        public bool processCommand(string command, Action<Processor> callback)
        {
            if (serial == null)
            {
                return false;
            }

            string[] commandParts = command.Split(' ');
            string trigger = commandParts[0];
            string value = commandParts[1];

            _romName = value;
            callback(this);

            // PinballY start animation
            if (_romName == "afm")
            {
                serial.Write(string.Format("{0} {1}\n", "S48", 1));
            }
            else
            {
                // TODO protect serial writes with try
                serial.Write(string.Format("{0} {1}\n", "S48", 0));
            }

            return true;
        }
    }
}
