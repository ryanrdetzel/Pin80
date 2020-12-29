using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pin80Server.Models
{
    public interface IAction
    {
        string name { get; set; }
        string id { get; set; }
    }
}
