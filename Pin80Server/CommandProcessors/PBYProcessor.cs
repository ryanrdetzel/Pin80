using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pin80Server.CommandProcessors
{
    class PBYProcessor : Processor
    {
        private SerialPort serial;
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
