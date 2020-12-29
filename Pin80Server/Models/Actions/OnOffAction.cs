namespace Pin80Server.Models.Actions
{
    public class OnOffAction : IAction
    {
        public string name { get; set; }
        public string id { get; set; }

        public int delay { get; set; }
        public int duration { get; set; }

        public OnOffAction(JSONSerializer.Action action)
        {
            name = action.name;
            id = action.id;
            delay = action.delay;
            duration = action.duration;
        }
        override public string ToString()
        {
            //return string.Format("({1}) {0} Delay:{2} Duration:{3}", name, id, delay, duration);
            return name;
        }
    }
}
