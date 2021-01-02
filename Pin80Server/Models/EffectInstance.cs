using Pin80Server.Models.Effects;
using System;
using System.Collections.Generic;

namespace Pin80Server.Models
{
    public class EffectInstance
    {
        public Target target { get; }
        public Effect effect { get; }
        public string triggeredValue { get; }

        public string dupeKey { get; }
        public long startedTimetamp { get; }
        public string id { get; }
        public long nextUpdate { get; set; }

        // Used to store anything need while running the effect
        public Dictionary<string, int> state = new Dictionary<string, int>();

        public EffectInstance(Target target, Effect effect, string triggeredValue)
        {
            this.target = target;
            this.effect = effect;
            this.triggeredValue = triggeredValue;

            id = Guid.NewGuid().ToString("N");
            dupeKey = string.Format("{0}{1}", target.id, effect.delay);

            // If there is a delay take that into consideration
            startedTimetamp = DateTimeOffset.Now.ToUnixTimeMilliseconds() + effect.delay;
        }

        public bool isValid()
        {
            return target != null && effect != null;
        }
    }
}
