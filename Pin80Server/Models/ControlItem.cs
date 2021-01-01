
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;


/**
* Control Item
*/
namespace Pin80Server.Models.JSONSerializer
{
    public class ControlItem
    {
        // Used so we can set the actual value for the disabled instead of the validated one.
        private bool _processing = false;

        public ControlItem(string trigger, string value)
        {
            this.enabled = false;
            this.value = value;
            _id = Guid.NewGuid().ToString("N");
            triggerString = trigger;
        }

        [OnDeserializing]
        internal void OnDeserializingMethod(StreamingContext context)
        {
            _processing = true;
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            _processing = false;
            if (!validate())
            {
                enabled = false;
            }
        }

        /* Prevent being enabled if we're missing data */
        private bool _enabled;
        [JsonProperty("enabled")]
        public bool enabled
        {
            get => _enabled;
            set
            {
                // Always allow false
                // Only set to true if it passes validate or it's processing
                if (!value || validate() || _processing)
                {
                    _enabled = value;
                }
            }
        }

        private string _id;
        [JsonProperty(PropertyName = "id")]
        public string id
        {
            get => _id;
            set
            {
                if (_processing) // Only allow when reading the file
                {
                    _id = value;
                }
            }
        }

        [JsonProperty(PropertyName = "trigger")]
        public string triggerString { get; set; }

        [JsonProperty("value")]
        public string value { get; set; }

        [JsonProperty("action")]
        public string actionString { get; set; }

        [JsonProperty("target")]
        public string targetString { get; set; }

        [JsonProperty("comment")]
        public string comment { get; set; }

        public bool validate()
        {
            if (triggerString != null && triggerString != "")
            {
                if (actionString != null && actionString != "")
                {
                    if (targetString != null && targetString != "")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", triggerString, actionString);
        }
    }
}
