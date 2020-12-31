using System.IO.Ports;

namespace Pin80Server.CommandProcessors
{
    public class PBYProcessor : Processor
    {
        public PBYProcessor(DataProcessor d, SerialPort s): base(d, s)
        {
        }

        override public bool processCommand(string command)
        {
            if (serial == null)
            {
                return false;
            }

            string[] commandParts = command.Split(' ');
            string trigger = commandParts[0];
            string value = commandParts[1];

            _romName = value;

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
