namespace Pin80Server.CommandProcessors
{
    internal interface Processor
    {
        bool processCommand(string command, MainForm mainForm);
        string romName();
    }
}
