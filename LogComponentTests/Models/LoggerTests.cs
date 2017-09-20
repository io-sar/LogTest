using System;
using System.Threading;
using LogTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace LogComponentTests.Models
{
    [TestClass]
    public class LoggerTests
    {
        [TestMethod]
        public void LoggerTest_StartLogger_FileCreate()
        {
            //Arrange
            string _loggerDirectory = @"C:\LogTestTests\LoggerTests\StartLogger";
            var logger = new Logger(_loggerDirectory);
            string filePath = $@"{_loggerDirectory}\Log{logger.GetDocumentServiceInstance().GetDocumentServiceCurrentTime().ToString("yyyyMMdd HHmmss fff")}.log";
            
            //Act
            logger.StartLogger();
            

            //Assert
            Assert.IsTrue(File.Exists(filePath));

        }

        [TestMethod]
        public void LoggerTest_WriteLogger_LogLineAdded()
        {
            //Arrange
            string _loggerDirectory = @"C:\LogTestTests\LoggerTests\WriteLogger";
            var logger = new Logger(_loggerDirectory);
            string filePath = $@"{_loggerDirectory}\Log{logger.GetDocumentServiceInstance().GetDocumentServiceCurrentTime().ToString("yyyyMMdd HHmmss fff")}.log";
            logger.StartLogger();
            
            //Act
            logger.Write("test");
            logger.StopWithoutFlush();


            //Assert
            Assert.IsTrue(File.ReadAllLines(filePath).Length>1);

        }

        [TestMethod]
        public void LoggerTest_StopWithoutFlush_LinesAreStopped()
        {
            //Arrange
            string _loggerDirectory = @"C:\LogTestTests\LoggerTests\WriteLogger";
            var logger = new Logger(_loggerDirectory);
            string filePath = $@"{_loggerDirectory}\Log{logger.GetDocumentServiceInstance().GetDocumentServiceCurrentTime().ToString("yyyyMMdd HHmmss fff")}.log";
            logger.StartLogger();

            //Act
            for (int i = 0; i < 5; i++)
            {
                logger.Write("test");

                if (i==2)
                {
                  logger.StopWithoutFlush();
                }
            }         

            //Assert
            Assert.IsTrue(File.ReadAllLines(filePath).Length == 4);

        }

        [TestMethod]
        public void LoggerTest_StopWithFlus_LogIsComplete()
        {
            //Arrange
            string _loggerDirectory = @"C:\LogTestTests\LoggerTests\WriteLogger";
            var logger = new Logger(_loggerDirectory);
            string filePath = $@"{_loggerDirectory}\Log{logger.GetDocumentServiceInstance().GetDocumentServiceCurrentTime().ToString("yyyyMMdd HHmmss fff")}.log";
            logger.StartLogger();

            //Act
            for (int i = 0; i < 5; i++)
            {
                logger.Write("test");

                if (i == 2)
                {
                    logger.StopWithFlush();
                }
            }

            //Assert
            Assert.IsTrue(File.ReadAllLines(filePath).Length == 6);

        }
    }
}
