using System;
using System.Collections.Concurrent;
using LogTest.Services;

namespace LogTest.Models
{
    public class Logger : ILogger
    {

        private readonly BlockingCollection<LogLine> _pendingLogLines;
        private readonly DocumentService _documentService;
        private readonly ThreadService _threadService;
        

        public Logger(string loggerDirectory)
        {
            _pendingLogLines = new BlockingCollection<LogLine>();
            _documentService = new DocumentService(loggerDirectory);
            _threadService = new ThreadService(this);
        }

        public void StartLogger()
        {
            _threadService.CreateBackgroundThread();         
        }

        public void Write(string text)
        {
            _pendingLogLines.TryAdd(new LogLine() {Text = text, Timestamp = DateTime.Now});      
        }

        public void StopWithFlush()
        {
            _threadService.StopThreadServiceAndFlush();
        }

        public void StopWithoutFlush()
        {
            _threadService.StopThreadServiceAndWithoutFlush();
        }

        public BlockingCollection<LogLine> GetPendingQueueInstance()
        {
            return _pendingLogLines;
        }

        public DocumentService GetDocumentServiceInstance()
        {
            return _documentService;
        }
    }
}
