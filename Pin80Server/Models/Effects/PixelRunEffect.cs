using Pin80Server.Models.JSONSerializer;

namespace Pin80Server.Models.Effects
{
    public class PixelRunEffect : Effect
    {
        public PixelRunEffect(EffectSerializer effect) : base(effect)
        {
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
                case 1:
                    pixelTarget.updatePixel(onLedNumber, color, triggeredAction);

                    if (onLedNumber++ >= numberOfLeds - 1)
                    {
                        if (reverse)
                        {
                            onLedNumber = numberOfLeds - 1;
                            step = 2;
                        }
                        else
                        {
                            step = 3;
                        }
                    }
                    triggeredAction.nextUpdate = ts + msEach;
                    break;
                case 2: // reverse
                    pixelTarget.updatePixel(onLedNumber, reverseColor, triggeredAction);

                    if (onLedNumber-- <= 0)
                    {
                        step = 3;
                    }
                    triggeredAction.nextUpdate = ts + msEach;
                    break;
                case 3:
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
