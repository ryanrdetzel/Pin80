﻿using Pin80Server.CommandProcessors;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pin80Server.Models.Effects
{
    public class PixelEffect : Effect
    {

        public PixelEffect(JSONSerializer.EffectSerializer effect) : base(effect)
        {
        }

        public override ProcessorTask Handle(Target target)
        {
            PixelTarget pixelTarget = (PixelTarget)target;
            PixelColor color = colors[0];

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var task = Task.Run(async delegate
            {
                long effectStarted = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                await Task.Delay(TimeSpan.FromMilliseconds(delay));
                token.ThrowIfCancellationRequested();

                pixelTarget.updateAllPixels(color, effectStarted);

                await Task.Delay(TimeSpan.FromMilliseconds(duration)); //TODO CHANGE ME to loop so it's more accurate

                token.ThrowIfCancellationRequested();
                pixelTarget.updateAllPixels(PixelColor.Black, effectStarted);
            }, token);

            return new ProcessorTask(task, tokenSource);
        }
        public override string ToString()
        {
            PixelColor color = colors[0];

            if (name != null)
            {
                return name;
            }

            string timeStr = timeString(duration);

            string str = string.Format("Pixel On {1} for {0}", timeStr, color);
            if (delay > 0)
            {
                str += string.Format(" w/ {0} delay", timeString(delay));
            }
            return str;
        }
    }
}