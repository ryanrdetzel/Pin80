using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public override bool Equals(object obj)
        {
            // TODO we should check the obj type
            var otherColor = (PixelColor)obj;
            return (otherColor.red == red & otherColor.blue == blue && otherColor.green == green);
        }

        public override int GetHashCode()
        {
            return int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
        }
    }
}
