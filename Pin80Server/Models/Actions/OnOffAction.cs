using Pin80Server.Models.JSONSerializer;
using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace Pin80Server.Models.Actions
{
    public class OnOffAction : Action
    {
        public OnOffAction(ActionSerializer action) : base(action)
        {
        }

        public override void Handle(string value, ControlItem item, Trigger trigger, Target target, SerialPort serial)
        {
            var port = target.port;

            // TODO If there is a delay work with that first
            Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromMilliseconds(delay));

                serial.Write(string.Format("{0} ON\n", port));

                await Task.Delay(TimeSpan.FromMilliseconds(duration));
                serial.Write(string.Format("{0} OFF\n", port));
            });
        }
    }
}
