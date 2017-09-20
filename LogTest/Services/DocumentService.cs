using System;
using LogTest.Models;
using LogTest.Helpers;

namespace LogTest.Services
{
    public class DocumentService
    {
      
        private DateTime _curDate;
        readonly DocumentHelper _dHelper;

        public DocumentService(string loggerDirectory)
        {
            _curDate = DateTime.Now;
            _dHelper = new DocumentHelper(loggerDirectory);
        }

        public void CreateLogFile()
        { 
           _dHelper.CreateDirectory();
           _dHelper.CreateFile(_curDate);
        }

        public void AddLogLine(LogLine logLine)
        {
            try
            {              
                _curDate = _dHelper.DateCheckAndCreateFile(_curDate, logLine.Timestamp);
                _dHelper.AddLineToFile(logLine);
            }
            catch (Exception e)
            {
                _dHelper.AddLineToFile(new LogLine() { Text = e.ToString(), Timestamp = DateTime.Now });
            }
            
        }

        public DateTime GetDocumentServiceCurrentTime()
        {
            return _curDate;
        }
    }
}
