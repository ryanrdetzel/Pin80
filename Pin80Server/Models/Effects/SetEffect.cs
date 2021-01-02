using Pin80Server.Models.JSONSerializer;

namespace Pin80Server.Models.Effects
{
    public class SetEffect : Effect
    {
        public SetEffect(EffectSerializer effect) : base(effect) { }

        public override bool Tick(EffectInstance triggeredAction, long ts)
        {
            var ledTarget = (LEDTarget)triggeredAction.target;
            int value = triggeredAction.triggeredValue == "1" ? 1 : 0;

            bool runAgain = true;
            triggeredAction.state.TryGetValue(Constants.STEP, out int step);

            switch (step)
            {
                case 0:
                    triggeredAction.nextUpdate = ts + delay;  // This makes a very small delay, we could skip this if there is no delay
                    break;
                case 1:
                    ledTarget.updatePortValue(value);
                    runAgain = false;
                    break;
            }

            triggeredAction.state[Constants.STEP] = step + 1;
            return runAgain;
        }

        public override string ToString()
        {
            if (name != null)
            {
                return name;
            }

            string timeStr = timeString(duration);

            string str = string.Format("Set Value", timeStr);
            if (delay > 0)
            {
                str += string.Format(" w/ {0} delay", timeString(delay));
            }
            return str;
        }
    }
}
