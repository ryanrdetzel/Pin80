using Pin80Server.CommandProcessors;
using Pin80Server.Models.JSONSerializer;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace Pin80Server.Models
{

    public class PixelColor
    {
        public static PixelColor Black = new PixelColor("000000");

        public int red;
        public int green;
        public int blue;

        public PixelColor(int red, int green, int blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public PixelColor(string hex)
        {
            red = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            green = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            blue = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        }

        public string hexValue
        {
            get
            {
                {
                    var rh = red.ToString("X").PadLeft(2, '0');
                    var gh = green.ToString("X").PadLeft(2, '0');
                    var bh = blue.ToString("X").PadLeft(2, '0');
                    return string.Format("{0}{1}{2}", rh, gh, bh);
                }
            }
        }

        public string nameForColor
        {
            get
            {
                switch (hexValue)
                {
                    case "000016": return "Dim Blue";
                    case "001600": return "Dim Green";
                    case "160000": return "Dim Red";
                }

                return hexValue;
            }
        }

        public void dimBy(int percent)
        {
            red = (int)((1 - (percent / 100.0)) * red);
            green = (int)((1 - (percent / 100.0)) * green);
            blue = (int)((1 - (percent / 100.0)) * blue);
            //Debug.WriteLine(hexValue);
        }

        public bool isOff()
        {
            return red == 0 && green == 0 && blue == 0;
        }

        override public string ToString()
        {
            return nameForColor;
        }
    }

    public class Pixel
    {
        public PixelColor color;
        public int steps;
        public int num;

        public Pixel(int num, PixelColor color, int step)
        {
            this.color = color;
            this.steps = step;
            this.num = num;
        }
    }

    public abstract class Action
    {
        public string name { get; set; }
        public string id { get; set; }
        public string kind { get; set; }
        public List<PixelColor> colors { get; set; }
        public int delay { get; set; }
        public int duration { get; set; }
        public int speed { get; set; }
        public bool reverse { get; set; }

        public abstract ProcessorTask Handle(string value, ControlItem item, Trigger trigger, Target target, SerialPort serial);

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
