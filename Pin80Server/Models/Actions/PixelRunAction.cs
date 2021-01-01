using Pin80Server.CommandProcessors;
using Pin80Server.Models.JSONSerializer;
using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace Pin80Server.Models.Actions
{
    public class PixelRunAction : Action
    {
        public PixelRunAction(ActionSerializer action) : base(action)
        {
        }

        public override string ToString()
        {
            if (name != null) return name;

            string timeStr = timeString(duration);
            PixelColor color = colors[0];

            string str = string.Format("Pixel Runner {1} for {0}", timeStr, color.nameForColor);
            if (delay > 0)
            {
                str += string.Format(" w/ {0} delay", timeString(delay));
            }
            if (reverse)
            {
                str += string.Format(" then reverse", timeString(delay));
                if (colors.Count == 2)
                {
                    str += string.Format(" in {0}", colors[1].nameForColor);
                }
            }

            return str;
        }

        public override ProcessorTask Handle(string value, ControlItem item, Trigger trigger, Target target, SerialPort serial)
        {
            var port = target.port;
            int numberOfLeds = target.leds;
            int startRange = 0;
            int endRange = target.leds - 1;

            PixelColor color = colors[0];

            //Figure out how long each pixel has based on count and duration
            int msEach = duration / numberOfLeds;

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var task = Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromMilliseconds(delay));
                token.ThrowIfCancellationRequested();

                for (int x = 0; x < numberOfLeds; x++)
                {
                    serial.Write(string.Format("{0} PXSTART\n", port));
                    serial.Write(string.Format("{0} PX{1} {2}\n", port, x, color.hexValue));
                    serial.Write(string.Format("{0} PXEND\n", port));

                    await Task.Delay(TimeSpan.FromMilliseconds(msEach));
                    token.ThrowIfCancellationRequested();
                }
                if (reverse)
                {
                    color = PixelColor.Black;
                    if (colors.Count == 2)
                    {
                        color = colors[1];
                    }
                    for (int x = numberOfLeds; x >= 0; x--)
                    {
                        serial.Write(string.Format("{0} PXSTART\n", port));
                        serial.Write(string.Format("{0} PX{1} {2}\n", port, x, color.hexValue));
                        serial.Write(string.Format("{0} PXEND\n", port));

                        await Task.Delay(TimeSpan.FromMilliseconds(msEach));
                        token.ThrowIfCancellationRequested();
                    }
                }
                // All off
                color = PixelColor.Black;
                serial.Write(string.Format("{0} PXSTART\n", port));
                serial.Write(string.Format("{0} PX{1}-{2} {3}\n", port, startRange, endRange, color.hexValue));
                serial.Write(string.Format("{0} PXEND\n", port));
            }, token);

            return new ProcessorTask(task, tokenSource);
        }
    }
}
