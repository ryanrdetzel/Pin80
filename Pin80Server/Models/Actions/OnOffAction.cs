using Pin80Server.CommandProcessors;
using Pin80Server.Models.JSONSerializer;
using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace Pin80Server.Models.Actions
{
    public class OnOffAction : Action
    {
        public OnOffAction(ActionSerializer action) : base(action)
        {
        }

        public override string ToString()
        {
            if (name != null) return name;

            string timeStr = timeString(duration);

            string str = string.Format("On for {0}", timeStr);
            if (delay > 0)
            {
                str += string.Format(" w/ {0} delay", timeString(delay));
            }
            return str;
        }

        public override ProcessorTask Handle(string value, ControlItem item, Trigger trigger, Target target, SerialPort serial)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var port = target.port;

            // TODO If there is a delay work with that first
            var task = Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromMilliseconds(delay));
                token.ThrowIfCancellationRequested();

                serial.Write(string.Format("{0} ON\n", port));

                await Task.Delay(TimeSpan.FromMilliseconds(duration));
                token.ThrowIfCancellationRequested();

                serial.Write(string.Format("{0} OFF\n", port));
            }, token);

            return new ProcessorTask(task, tokenSource);
        }
    }
}
