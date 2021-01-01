namespace Pin80Server.Models.JSONSerializer
{
    public class TargetSerializer
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
    }
}
