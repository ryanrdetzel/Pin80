using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pin80Server.Models
{
    public class LEDTarget : Target
    {
        private int _portValue;
        private readonly object _valueLock = new object();

        private bool _hasUpdate;

        public override bool hasUpdate
        {
            get
            {
                return _hasUpdate;
            }
        }

        public LEDTarget(JSONSerializer.TargetSerializer target) : base(target)
        {
            _hasUpdate = false;
        }

        override public void Run(SerialPort serialPort)
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
