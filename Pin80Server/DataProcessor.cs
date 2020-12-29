using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Pin80Server
{
    public class DataProcessor
    {
        private List<ControlItem> data = new List<ControlItem>();

        public DataProcessor()
        {
            data = LoadFile();
        }

        public List<ControlItem> getProcessedData()
        {
            return data;
        }

        private List<ControlItem> LoadFile()
        {
            using (StreamReader r = new StreamReader("example.json"))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<ControlItem>>(json);
            }
        }
    }
}
