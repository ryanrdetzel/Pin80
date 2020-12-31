using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

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

    public class ProcessorTask
    {
        public Task task { get; set; }
        public CancellationTokenSource token { get; set; }

        public ProcessorTask(Task task, CancellationTokenSource token)
        {
            this.task = task;
            this.token = token;
        }
    }
}
