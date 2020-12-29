using Newtonsoft.Json;

namespace Pin80Server.Models.JSONSerializer
{
    public class Action
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("kind")]
        public string kind { get; set; }

        [JsonProperty("delay")]
        public int delay { get; set; }

        [JsonProperty("duration")]
        public int duration { get; set; }

        [JsonProperty("speed")]
        public int speed { get; set; }
    }
}
