using Pin80Server.CommandProcessors;
using Pin80Server.Models.JSONSerializer;
using System.Collections.Generic;
using System.Linq;

namespace Pin80Server.Models
{
    public abstract class Action
    {
        public string name { get; set; }
        public string id { get; set; }
        public string kind { get; set; }
        public List<PixelColor> colors { get; set; }
        public int delay { get; set; }
        public int duration { get; set; }
        public int speed { get; set; } // TODO use or remove
        public bool reverse { get; set; }

        public abstract ProcessorTask Handle(Target target);

        public Action(JSONSerializer.ActionSerializer action)
        {
            name = action.name;
            id = action.id;
            kind = action.kind;
            colors = new List<PixelColor>();

            if (action.colors != null)
            {
                colors.AddRange(action.colors.Select(cs => new PixelColor(cs)).ToList());
            }

            delay = action.delay;   // TODO Set reasonable limts 
            duration = (action.duration > 0) ? action.duration : 200;
            speed = action.speed; // TODO not using.
            reverse = action.reverse;
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

        /* Checks the value to see if this action should fire */
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
