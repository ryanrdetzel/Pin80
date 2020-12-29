using System;

namespace Pin80Server.CommandProcessors
{
    internal interface Processor
    {
        bool processCommand(string command, Action<Processor> callback);
        string romName();
    }
}
