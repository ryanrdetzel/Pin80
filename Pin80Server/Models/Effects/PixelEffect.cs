namespace Pin80Server.Models.Effects
{
    public class PixelEffect : Effect
    {

        public PixelEffect(JSONSerializer.EffectSerializer effect) : base(effect)
        {
        }

        public override bool Tick(EffectInstance triggeredAction, long ts)
        {
            PixelTarget pixelTarget = (PixelTarget)triggeredAction.target;
            PixelColor color = colors[0];

            triggeredAction.state.TryGetValue(Constants.STEP, out int step);

            bool runAgain = true;

            switch (step)
            {
                case 0:
                    triggeredAction.nextUpdate = ts + delay;  // This makes a very small delay, we could skip this if there is no delay
                    break;
                case 1:
                    pixelTarget.updateAllPixels(color, triggeredAction);
                    triggeredAction.nextUpdate = ts + duration;
                    break;
                case 2:
                    pixelTarget.updateAllPixels(PixelColor.Black, triggeredAction);
                    runAgain = false;
                    break;
            }

            triggeredAction.state[Constants.STEP] = step + 1;
            return runAgain;
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
