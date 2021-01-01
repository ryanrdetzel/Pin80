using Pin80Server.Models;
using System.Collections.Generic;
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

        public abstract List<EffectInstance> processCommand(string command, long ts);
        public void setMainForm(MainForm mf)
        {
            mainForm = mf;
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
