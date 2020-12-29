using System;

namespace Pin80Server.CommandProcessors
{
    interface Processor
    {
        bool processCommand(string command, Action<Processor> callback);
        string romName();
    }
}
