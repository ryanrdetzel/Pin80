using Newtonsoft.Json;

namespace Pin80Server.Models.JSONSerializer
{
    public class Target
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("port")]
        public string port { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
