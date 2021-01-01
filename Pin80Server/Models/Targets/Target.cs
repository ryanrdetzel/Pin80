﻿using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace Pin80Server.Models
{
    public abstract class Target
    {
        public string id { get; set; }
        public string name { get; set; }
        public string kind { get; set; }
        public string port { get; set; }

        public abstract bool hasUpdate { get; }

        //private string exclusiveId; // Only this effect can change the target until it's released.

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

        //public bool makeExclusive(out string exclusiveId)
        //{
        //    if (this.exclusiveId == null)
        //    {
        //        this.exclusiveId = exclusiveId = Guid.NewGuid().ToString("N");
        //        return true;
        //    }
        //    exclusiveId = null;
        //    return false;
        //}

        public abstract void Run(SerialPort serialPort);

        public List<string> validEffects()
        {
            return validEffects(kind);
        }

        public static List<string> validEffects(string kind)
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

        public override string ToString()
        {
            return name;
        }

    }
}
