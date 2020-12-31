namespace Pin80Server.CommandProcessors
{
    internal interface IProcessor
    {
        bool processCommand(string command);
        void setMainForm(MainForm mf);
        string romName();
    }
}
