
using Newtonsoft.Json;
/**
* Control Item
*/
namespace Pin80Server.Models.JSONSerializer
{
    public class ControlItem
    {
        [JsonProperty("enabled")]
        public bool enabled { get; set; }

        [JsonProperty("trigger")]
        public string trigger { get; set; }

        [JsonProperty("action")]
        public string action { get; set; }

        [JsonProperty("target")]
        public string target { get; set; }

        [JsonProperty("comment")]
        public string comment { get; set; }

        [JsonProperty("value")]
        public string value { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", trigger, action);
        }
    }
}
