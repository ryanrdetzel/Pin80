using Pin80Server.Models.JSONSerializer;
using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading.Tasks;

namespace Pin80Server.Models.Actions
{
    public class PixelAction : Action
    {
        public int delay { get; set; }
        public int duration { get; set; }

        public PixelAction(JSONSerializer.Action action)
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

        public override void Handle(string value, ControlItem item, Trigger trigger, Target target, SerialPort serial)
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
