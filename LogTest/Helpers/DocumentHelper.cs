using System;
using System.IO;
using System.Text;
using LogTest.Models;

namespace LogTest.Helpers
{
    public class DocumentHelper
    {
        private StreamWriter _writer;
        private readonly string _defaultDirectory;

        public DocumentHelper(string loggerDirectory)
        {
            _defaultDirectory = loggerDirectory;
        }

        public void CreateDirectory()
        {
            if (!Directory.Exists(_defaultDirectory)) Directory.CreateDirectory(_defaultDirectory);
        }

        public void CreateFile(DateTime curDate)
        {
            try
            {
                _writer = File.AppendText($@"{_defaultDirectory}\Log{curDate.ToString("yyyyMMdd HHmmss fff")}.log");
                _writer.Write("Timestamp".PadRight(25, ' ') + "\t" + "Data".PadRight(15, ' ') + "\t" + Environment.NewLine);
                _writer.AutoFlush = true;
            }
            catch (Exception)
            {
            }
                         
        }

        public void AddLineToFile(LogLine logLine)
        {
            
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(logLine.Timestamp.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            stringBuilder.Append("\t");
            stringBuilder.Append(logLine.LineText());
            stringBuilder.Append("\t");
            stringBuilder.Append(Environment.NewLine);

           
           _writer.Write(stringBuilder.ToString());
                 
        }

        public DateTime DateCheckAndCreateFile(DateTime currentDate, DateTime logLineDate)
        {
            try
            {
                if (currentDate.Day != logLineDate.Day)
                {
                    CreateFile(logLineDate);
                    return logLineDate;
                }       
            }
            catch (Exception)
            {
            }
            return currentDate;
        }
        
    }
}
