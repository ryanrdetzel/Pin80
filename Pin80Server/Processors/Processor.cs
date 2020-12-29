using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pin80Server.CommandProcessors
{
    interface Processor
    {
        bool processCommand(string command, Action<Processor> callback);
        string romName();
    }
}
