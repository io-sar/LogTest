using System;
using System.Threading;
using LogTest.Models;

namespace LogTest.Services
{
    public class ThreadService
    {
        private readonly Logger _logger;
        private Thread _runningThread;

        readonly ManualResetEvent _waitEvent = new ManualResetEvent(false);
        private readonly ManualResetEvent _terminateEvent = new ManualResetEvent(false);


        public ThreadService(Logger logger)
        {
            _logger = logger;
        }

        public void CreateBackgroundThread()
        {
            _logger.GetDocumentServiceInstance().CreateLogFile();
           

            var runningThread = new Thread(MainLoop);
            runningThread.IsBackground = true;
            runningThread.Name = "MainLoopThread";
            runningThread.Start();

            _runningThread = runningThread;
        }

        private void MainLoop()
        {
            
            var cancellationEvent  = new CancellationTokenSource();
            LogLine logLine ;
            _waitEvent.Reset();

            while (_logger.GetPendingQueueInstance().TryTake(out logLine, Timeout.Infinite, cancellationEvent.Token))
            {
                _logger.GetDocumentServiceInstance().AddLogLine(logLine);
                _waitEvent.Set();
            }

        }

        public void StopThreadServiceAndFlush()
        {
            _waitEvent.WaitOne();
                       
        }

        public void StopThreadServiceAndWithoutFlush()
        {
            _runningThread.Join();
        }

    }
}