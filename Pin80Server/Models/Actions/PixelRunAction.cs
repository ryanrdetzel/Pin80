using Pin80Server.CommandProcessors;
using Pin80Server.Models.JSONSerializer;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pin80Server.Models.Actions
{
    public class PixelRunAction : Action
    {
        public PixelRunAction(ActionSerializer action) : base(action)
        {
        }

        public override ProcessorTask Handle(Target target)
        {
            PixelTarget pixelTarget = (PixelTarget)target;
            PixelColor color = colors[0];

            int numberOfLeds = pixelTarget.leds;

            int msEach = duration / numberOfLeds;

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var task = Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromMilliseconds(delay));
                token.ThrowIfCancellationRequested();

                long actionStarted = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                var nextUpdate = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                bool running = true;

                int onLedNumber = 0;

                while (running)
                {
                    var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    if (now >= nextUpdate)
                    {
                        token.ThrowIfCancellationRequested();

                        pixelTarget.updatePixel(onLedNumber, color, actionStarted);

                        if (onLedNumber++ >= numberOfLeds - 1)
                        {
                            running = false;
                        }
                        nextUpdate = now + msEach;
                    }
                }


                if (reverse)
                {
                    running = true;
                    color = (colors.Count == 2) ? colors[1] : PixelColor.Black;
                    onLedNumber = numberOfLeds - 1;

                    while (running)
                    {
                        var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                        if (now >= nextUpdate)
                        {
                            token.ThrowIfCancellationRequested();

                            pixelTarget.updatePixel(onLedNumber, color, actionStarted);

                            if (onLedNumber-- <= 0)
                            {
                                running = false;
                            }
                            nextUpdate = now + msEach;
                        }
                    }
                }
                else
                {
                    while (DateTimeOffset.Now.ToUnixTimeMilliseconds() < nextUpdate)
                    {
                        ;
                    }

                    pixelTarget.updateAllPixels(PixelColor.Black, actionStarted);
                }
            }, token);

            return new ProcessorTask(task, tokenSource);
        }

        public override string ToString()
        {
            if (name != null)
            {
                return name;
            }

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
    }
}
