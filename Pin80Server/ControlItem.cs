
using Newtonsoft.Json;
/**
* Control Item
*/
namespace Pin80Server
{
    public class ControlItem
    {
        [JsonProperty("enabled")] 
        public  bool enabled { get; set; }
        [JsonProperty("trigger")]
        public string trigger { get; set; }
        [JsonProperty("action")]
        public string action { get; set; }
        [JsonProperty("comment")]
        public string comment { get; set; }

        override public string ToString()
        {
            return string.Format("{0} {1}", trigger, action);
        }
    }
}
