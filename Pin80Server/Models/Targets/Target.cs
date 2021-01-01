﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pin80Server.Models
{
    public abstract class Target
    {
        public string id { get; set; }
        public string name { get; set; }
        public string kind { get; set; }
        public string port { get; set; }

        public abstract bool hasUpdate { get; }

        public Target(string id, string name, string kind, string port)
        {
            this.id = id;
            this.name = name;
            this.kind = kind;
            this.port = port;
        }

        public Target(JSONSerializer.TargetSerializer target)
        {
            this.id = target.id;
            this.name = target.name;
            this.kind = target.kind;
            this.port = target.port;
        }

        
        public abstract void Run(SerialPort serialPort);

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

        override public string ToString()
        {
            return name;
        }

    }
}
