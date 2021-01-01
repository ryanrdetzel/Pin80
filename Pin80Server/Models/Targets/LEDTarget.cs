using System.IO.Ports;

namespace Pin80Server.Models
{
    public class LEDTarget : Target
    {
        private int _portValue;
        private readonly object _valueLock = new object();

        private bool _hasUpdate;

        public override bool hasUpdate => _hasUpdate;

        public LEDTarget(JSONSerializer.TargetSerializer target) : base(target)
        {
            _hasUpdate = false;
        }

        public override void Run(SerialPort serialPort)
        {
            string value = (_portValue == 0) ? "OFF" : "ON";
            serialPort.Write(string.Format("{0} {1}\n", port, value));
            lock (_valueLock)
            {
                _hasUpdate = false;
            }
        }

        public void updatePortValue(int value)
        {
            lock (_valueLock)
            {
                _portValue = value;
                _hasUpdate = true;
            }
        }
    }
}
