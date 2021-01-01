using System.Collections.Generic;

namespace Pin80Server.Models.JSONSerializer
{
    public class Target
    {
        public string id { get; set; }

        public string name { get; set; }
        public string kind { get; set; }

        public string port { get; set; }

        public int leds { get; set; }
        public override string ToString()
        {
            return name;
        }

        public List<string> validActions()
        {
            return validActions(kind);
        }

        public static List<string> validActions(string kind)
        {
            var list = new List<string>();

            switch (kind)
            {
                case "PIXEL":
                    list.Add("PIXELRUN");
                    list.Add("PIXEL");
                    list.Add("PIXELCOMIT");
                    break;
                case "LED":
                    list.Add("ONOFF");
                    break;
            }
            return list;
        }
    }
}
