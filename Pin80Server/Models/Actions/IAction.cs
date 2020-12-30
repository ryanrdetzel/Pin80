using Pin80Server.Models.JSONSerializer;
using System.IO.Ports;

namespace Pin80Server.Models
{
    public interface IAction
    {
        string name { get; set; }
        string id { get; set; }

        void handle(string value, ControlItem item, Trigger trigger, Target target, SerialPort serial);
    }
}
