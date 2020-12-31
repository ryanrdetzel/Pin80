using System.IO.Ports;

namespace Pin80Server.CommandProcessors
{
    public abstract class Processor
    {
        protected readonly SerialPort serial;
        protected readonly DataProcessor dataProcessor;
        protected MainForm mainForm;
        protected string _romName;

        public Processor(DataProcessor d, SerialPort s)
        {
            serial = s;
            dataProcessor = d;
        }

        public abstract bool processCommand(string command);
        public void setMainForm(MainForm mf)
        {
            mainForm = mf;
        }

        string romName()
        {
            return _romName;
        }
    }
}
