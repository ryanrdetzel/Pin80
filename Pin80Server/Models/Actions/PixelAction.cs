using Pin80Server.Models.JSONSerializer;
using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace Pin80Server.Models.Actions
{
    public class PixelAction : Action
    {
        public string color { get; set; }

        public PixelAction(JSONSerializer.ActionSerializer action) : base(action)
        {
            color = action.colors[0];
        }

        public override string ToString()
        {
            if (name != null) return name;

            string timeStr = timeString(duration);

            string str = string.Format("Pixel On {1} for {0}", timeStr, nameForColor(color));
            if (delay > 0)
            {
                str += string.Format(" w/ {0} delay", timeString(delay));
            }
            return str;
        }

        public override void Handle(string value, ControlItem item, Trigger trigger, Target target, SerialPort serial)
        {
            var port = target.port;
            int startRange = 0;
            int endRange = target.leds - 1;

            //TODO check that the range makes sense.

            // Calculate all the pixel colors and send them.
            string OnCmd = string.Format("{0} PX{1}-{2} {3}\n", port, startRange, endRange, color);
            string OffCmd = string.Format("{0} PX{1}-{2} {3}\n", port, startRange, endRange, "000000");

            Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromMilliseconds(delay));

                serial.Write(string.Format("{0} PXSTART\n", port)); // Include how many we're updating?
                serial.Write(OnCmd);
                serial.Write(string.Format("{0} PXEND\n", port));

                await Task.Delay(TimeSpan.FromMilliseconds(duration));

                serial.Write(string.Format("{0} PXSTART\n", port));
                serial.Write(OffCmd);
                serial.Write(string.Format("{0} PXEND\n", port));
            });
        }
    }
}
