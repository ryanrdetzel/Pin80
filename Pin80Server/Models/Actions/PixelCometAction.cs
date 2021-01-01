using Pin80Server.CommandProcessors;
using Pin80Server.Models.JSONSerializer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pin80Server.Models.Actions
{
    public class PixelComitAction : Action
    {
        public PixelComitAction(ActionSerializer action) : base(action)
        {
            var color1 = new PixelColor("001600");
            Debug.WriteLine(color1.hexValue);
        }

        public override string ToString()
        {
            if (name != null)
            {
                return name;
            }

            string timeStr = timeString(duration);
            PixelColor color = colors[0];

            string str = string.Format("Pixel Comit {1} for {0}", timeStr, color.nameForColor);
            if (delay > 0)
            {
                str += string.Format(" w/ {0} delay", timeString(delay));
            }

            return str;
        }

        public override ProcessorTask Handle(Target target)
        {
            PixelTarget pixelTarget = (PixelTarget)target;

            var port = target.port;
            int numberOfLeds = pixelTarget.leds;
            int endRange = pixelTarget.leds - 1;

            PixelColor color = colors[0];

            //Figure out how long each pixel has based on count and duration
            int msEach = duration / numberOfLeds;

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var task = Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromMilliseconds(delay));
                token.ThrowIfCancellationRequested();

                long actionStarted = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                Debug.WriteLine(string.Format("Action started: {0}", actionStarted));

                // Start with x pixels in array.
                // Starting with 0, full brightness at color
                // every iteration subtract little off each in the array and resend.
                // End when all pixels are off.
                List<Pixel> pixels = new List<Pixel>(numberOfLeds);

                pixels.Add(new Pixel(0, new PixelColor(255, 0, 0)));

                int pixelsAdded = 1;

                var nextUpdate = DateTimeOffset.Now.ToUnixTimeMilliseconds() + msEach;

                while (pixels.Count > 0)
                {
                    var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    if (now >= nextUpdate)
                    {
                        nextUpdate = now + msEach;
                        //Debug.WriteLine(DateTimeOffset.Now.ToUnixTimeMilliseconds());
                        //serial.Write(string.Format("{0} PXSTART\n", port));

                        // Check pixels
                        foreach (var ll in pixels.ToList())
                        {
                            if (ll.color.isOff())
                            {
                                pixels.Remove(ll);
                            }
                            else
                            {
                                ll.color.dimBy(20);
                            }
                            //serial.Write(string.Format("{0} PX{1} {2}\n", port, ll.num, ll.color.hexValue));
                        }

                        // serial.Write(string.Format("{0} PXEND\n", port));

                        if (pixelsAdded < numberOfLeds)
                        {
                            pixels.Add(new Pixel(pixelsAdded++, new PixelColor(255, 0, 0)));
                        }

                        token.ThrowIfCancellationRequested();
                    }
                    /// Maybe small delay here?
                }
            }, token);

            return new ProcessorTask(task, tokenSource);
        }
    }
}
