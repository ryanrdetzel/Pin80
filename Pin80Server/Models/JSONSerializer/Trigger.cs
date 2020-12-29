﻿using Newtonsoft.Json;

namespace Pin80Server.Models.JSONSerializer
{
    public class Trigger
    {
        [JsonProperty("command")]
        public string command { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        public override string ToString()
        {
            return command;
        }
    }
}
