using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pin80Server.Models.JSONSerializer
{
    public class ActionSerializer
    {
        [JsonProperty(Required = Required.Always)]
        public string id { get; set; }
        public string name { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string kind { get; set; }
        public int delay { get; set; }
        public int duration { get; set; }
        public int speed { get; set; }
        public bool reverse { get; set; }
        public bool interpolate { get; set; } //TODO

        public List<string> colors { get; set; }
    }
}
