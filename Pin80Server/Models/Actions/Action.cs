using Pin80Server.Models.JSONSerializer;
using System.IO.Ports;

namespace Pin80Server.Models
{
    public abstract class Action
    {
        public string name { get; set; }
        public string id { get; set; }

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

        public abstract void Handle(string value, ControlItem item, Trigger trigger, Target target, SerialPort serial);
    }
}
