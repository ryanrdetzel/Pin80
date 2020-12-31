using Pin80Server.Models.JSONSerializer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading.Tasks;

namespace Pin80Server.Models.Actions
{
    public class PixelRunAction : Action
    {
        //public int leds { get; set; }

        public PixelRunAction(ActionSerializer action): base(action)
        {
            //name = action.name;
            //id = action.id;
            //delay = action.delay;   // TODO Set reasonable limts 
            //duration = (action.duration > 0) ? action.duration : 200;
            //colors = action.colors;
            //reverse = action.reverse;
        }

        public override void Handle(string value, ControlItem item, Trigger trigger, Target target, SerialPort serial)
        {
            var port = target.port;
            int numberOfLeds = target.leds;
            int startRange = 0;
            int endRange = target.leds - 1;

            string color = colors[0];
            string off = "000000";

            //Figure out how long each pixel has based on count and duration
            int msEach = duration / numberOfLeds; 

            Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromMilliseconds(delay));

                for (int x = 0; x < numberOfLeds; x++)
                {
                    serial.Write(string.Format("{0} PXSTART\n", port));
                    serial.Write(string.Format("{0} PX{1} {2}\n", port, x, color));
                    serial.Write(string.Format("{0} PXEND\n", port));

                    await Task.Delay(TimeSpan.FromMilliseconds(msEach));
                }
                if (reverse)
                {
                    color = off;
                    if (colors.Count == 2)
                    {
                        color = colors[1];
                    }
                    for (int x = numberOfLeds; x >= 0 ; x--)
                    {
                        serial.Write(string.Format("{0} PXSTART\n", port));
                        serial.Write(string.Format("{0} PX{1} {2}\n", port, x, color));
                        serial.Write(string.Format("{0} PXEND\n", port));

                        await Task.Delay(TimeSpan.FromMilliseconds(msEach));
                    }
                }
                // All off
                color = off;
                serial.Write(string.Format("{0} PXSTART\n", port));
                serial.Write(string.Format("{0} PX{1}-{2} {3}\n", port, startRange, endRange, color));
                serial.Write(string.Format("{0} PXEND\n", port));
            });
        }
    }
}
