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

        public override ProcessorTask Handle(Target target)
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var ledTarget = (LEDTarget)target;

            var port = target.port;

            var nextUpdate = DateTimeOffset.Now.ToUnixTimeMilliseconds() + duration;
            bool running = true;

            // TODO If there is a delay work with that first
            var task = Task.Run(async delegate
            {
                await Task.Delay(TimeSpan.FromMilliseconds(delay));
                token.ThrowIfCancellationRequested();

                ledTarget.updatePortValue(1);

                while (running)
                {
                    var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                    if (now >= nextUpdate)
                    {
                        token.ThrowIfCancellationRequested();
                        ledTarget.updatePortValue(0);
                        running = false;
                    }
                }
            }, token);

            return new ProcessorTask(task, tokenSource);
        }
    }
}
