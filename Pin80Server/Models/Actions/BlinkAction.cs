
using Pin80Server.Models.JSONSerializer;
using System.IO.Ports;

namespace Pin80Server.Models.Actions
{
    public class BlinkAction : IAction
    {
        public string name { get; set; }
        public string id { get; set; }
        public int delay { get; set; }
        public int duration { get; set; }
        public int speed { get; set; }

        public BlinkAction(JSONSerializer.Action action)
        {
            name = action.name;
            id = action.id;
            delay = action.delay;
            duration = action.duration;
            speed = action.speed;
        }
        public override string ToString()
        {
            //return string.Format("({1}) {0} Delay:{2} Duration:{3} speed:{4}", name, id, delay, duration, speed);
            return name;
        }

        public void handle(string value, ControlItem item, Trigger trigger, Target target, SerialPort serial)
        {
            throw new System.NotImplementedException();
        }
    }
}
