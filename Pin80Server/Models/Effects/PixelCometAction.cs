﻿using Pin80Server.Models.JSONSerializer;

namespace Pin80Server.Models.Effects
{
    public class PixelCometEffect : Effect
    {
        public PixelCometEffect(EffectSerializer effect) : base(effect)
        {
            if (fade < 5 || fade > 90)
            {
                fade = 5;
            }
        }

        public override bool Tick(EffectInstance triggeredAction, long ts)
        {
            PixelTarget pixelTarget = (PixelTarget)triggeredAction.target;
            PixelColor color = colors[0];
            PixelColor reverseColor = (colors.Count == 2) ? colors[1] : PixelColor.Black;

            int numberOfLeds = pixelTarget.leds;
            int msEach = duration / numberOfLeds;

            int step, onLedNumber;
            triggeredAction.state.TryGetValue("step", out step);
            triggeredAction.state.TryGetValue("onLedNumber", out onLedNumber);

            bool runAgain = true;

            switch (step)
            {
                case 0: // delay
                    triggeredAction.nextUpdate = ts + delay;  // This makes a very small delay, we could skip this if there is no delay
                    step = 1;
                    break;
                case 1: // first pixel up
                    pixelTarget.fadeAllPixels(fade);

                    if (onLedNumber < numberOfLeds)
                    {
                        pixelTarget.updatePixel(onLedNumber, color, triggeredAction);
                        onLedNumber++;
                    }

                    if (onLedNumber == numberOfLeds && pixelTarget.lastPixel().isOff())
                    {
                        step = 2;
                    }
                    triggeredAction.nextUpdate = ts + msEach;
                    break;
                case 2:
                    pixelTarget.updateAllPixels(PixelColor.Black, triggeredAction);
                    runAgain = false;
                    break;
            }

            triggeredAction.state["step"] = step;
            triggeredAction.state["onLedNumber"] = onLedNumber;

            return runAgain;
        }

        public override string ToString()
        {
            if (name != null)
            {
                return name;
            }

            string timeStr = timeString(duration);
            PixelColor color = colors[0];

            string str = string.Format("Pixel Comet {1} for {0}", timeStr, color.nameForColor);
            if (delay > 0)
            {
                str += string.Format(" w/ {0} delay", timeString(delay));
            }

            return str;
        }
    }
}
