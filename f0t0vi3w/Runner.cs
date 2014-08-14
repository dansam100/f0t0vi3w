using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace f0t0vi3w3r.Runner
{
    class Runner
    {
        Process process;
        ManualResetEvent _event;
        private string paintprocess = "mspaint.exe";
        public Runner(string process, string arguments)
        {
            this.process = new Process();
            this.process.StartInfo = new ProcessStartInfo(process, arguments);
        }

        public Runner()
        { }

        public void StartPaint(string parameter)
        {
            this.process = new Process();
            //this.process.StartInfo = new ProcessStartInfo(paintprocess, parameter);
            this.process.StartInfo.FileName = paintprocess;
            this.process.StartInfo.Arguments = parameter;
            this.Start();
        }

        public void Start()
        {
            _event = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(this.Start, _event);
            WaitHandle.WaitAll(new WaitHandle[] { _event });
        }
        
        private void Start(object state)
        {
            this.process.Start();
            (state as ManualResetEvent).Set();
        }
    }
}
