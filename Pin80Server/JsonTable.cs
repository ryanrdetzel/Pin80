using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pin80Server
{
    class JsonTable
    {
       // public string tableName { get; set; }
       // public List<string> romNames { get; set; }

        public  bool Enabled { get; set; }
        public string Trigger { get; set; }
        public string Action { get; set; }
        override public string ToString()
        {
            return string.Format("{0} {1}", Trigger, Action);
        }

    }
}
