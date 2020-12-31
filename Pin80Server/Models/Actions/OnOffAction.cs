using Pin80Server.Models.JSONSerializer;
using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading.Tasks;

namespace Pin80Server.Models.Actions
{
    public class OnOffAction : IAction
    {
        public string name { get; set; }
        public string id { get; set; }

        public int delay { get; set; }
        public int duration { get; set; }

        public OnOffAction(JSONSerializer.Action action)
        {
            name = action.name;
            id = action.id;
            delay = action.delay;   // TODO Set reasonable limts 
            duration = (action.duration > 0) ? action.duration : 200;
        }

        public override string ToString()
        {
            return name;
        }

        void IAction.handle(string value, ControlItem item, Trigger trigger, Target target, SerialPort serial)
        {
            // TODO check serial, etc

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
                        return;
                    }
                }
                else if (item.value.StartsWith("<"))
                {
                    if (int.Parse(value) >= int.Parse(item.value.Remove(0, 1)))
                    {
                        //Debug.WriteLine(string.Format("{0} isn't greater than {1}", value, item.value));
                        return;
                    }
                }
                else // equals
                {
                    if (item.value != value)
                    {
                        //Debug.WriteLine(string.Format("{0} doesn't equal {1}", value, item.value));
                        return;
                    }
                }
            }

            if (target != null)
            {
                var port = target.port;

                // TODO If there is a delay work with that first
                serial.Write(string.Format("{0} ON\n", port));
                Task.Run(async delegate
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(duration));
                    serial.Write(string.Format("{0} OFF\n", port));
                });

            }
        }
    }
}
