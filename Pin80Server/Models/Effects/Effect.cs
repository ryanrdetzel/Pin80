using Pin80Server.Models.JSONSerializer;
using System.Collections.Generic;
using System.Linq;

namespace Pin80Server.Models.Effects
{
    public abstract class Effect
    {
        public string name { get; set; }
        public string id { get; set; }
        public string kind { get; set; }
        public List<PixelColor> colors { get; set; }
        public int delay { get; set; }
        public int duration { get; set; }
        public int speed { get; set; } // TODO use or remove
        public bool reverse { get; set; }
        public int fade { get; set; }

        public abstract bool Tick(EffectInstance triggeredAction, long ts);

        public Effect(JSONSerializer.EffectSerializer effect)
        {
            name = effect.name;
            id = effect.id;
            kind = effect.kind;
            colors = new List<PixelColor>();
            fade = effect.fade;

            if (effect.colors != null)
            {
                colors.AddRange(effect.colors.Select(cs => new PixelColor(cs)).ToList());
            }

            delay = effect.delay;   // TODO Set reasonable limts 
            duration = (effect.duration > 0) ? effect.duration : 200;
            speed = effect.speed; // TODO not using.
            reverse = effect.reverse;
        }

        public string timeString(int durationMs)
        {
            string timeStr = string.Format("{0}ms", durationMs);

            if (durationMs >= 1000)
            {
                double seconds = System.Math.Round(((float)durationMs / 1000), 3);
                timeStr = string.Format("{0}s", seconds);
            }
            return timeStr;
        }

        /* Checks the value to see if this effect should fire */
        public bool Validate(string value, ControlItem item)
        {
            // TODO Make this work for all cases.
            if (item.value != "Any") // TODO Move this to a constant
            {
                // TODO try/catch
                // Starts with <>
                if (item.value.StartsWith(">"))
                {
                    int valueI = int.Parse(value);
                    int valueI2 = int.Parse(item.value.Replace(">", ""));
                    //Debug.WriteLine(string.Format("{0}{1} - {2}{3}", value, valueI, item.value, valueI2));

                    if (valueI <= valueI2)
                    {
                        //Debug.WriteLine(string.Format("{0} isn't less than {1}", value, item.value));
                        return false;
                    }
                }
                else if (item.value.StartsWith("<"))
                {
                    if (int.Parse(value) >= int.Parse(item.value.Remove(0, 1)))
                    {
                        //Debug.WriteLine(string.Format("{0} isn't greater than {1}", value, item.value));
                        return false;
                    }
                }
                else // equals
                {
                    if (item.value != value)
                    {
                        //Debug.WriteLine(string.Format("{0} doesn't equal {1}", value, item.value));
                        return false;
                    }
                }
            }
            return true;
        }

        public override string ToString()
        {
            return name ?? id;
        }
    }
}
