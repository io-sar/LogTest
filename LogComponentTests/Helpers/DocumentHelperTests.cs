using System;
using System.Dynamic;
using System.IO;
using FluentAssertions;
using LogTest.Helpers;
using LogTest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LogComponentTests.Helpers
{
    [TestClass]
    public class DocumentHelperTests
    {
        private string _testDirectory = @"C:\LogTestTests\DocumentHelperTests";

        [TestMethod]
        public void AddLineToFile_LogLineIsNull_ThrowsNullReferenceException()
        {
            // Arrange
            var dh = new DocumentHelper(_testDirectory);
            dh.CreateDirectory();
            dh.CreateFile(DateTime.Now);
            LogLine logLine = null;

            // Act
            Action act = () => dh.AddLineToFile(logLine);

            // Assert
            act.ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void AddLineToFile_LogLineIsSet_StringFormat()
        {
            //Arrange
            var dh = new DocumentHelper(_testDirectory);
            dh.CreateDirectory();
            dh.CreateFile(DateTime.Now);
            LogLine logLine = new LogLine();

            // Act
            Action act = () => dh.AddLineToFile(logLine);

            // Assert
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void DateCheckAndCreateFile_DayIsEqual_NoNewfileIsCreated()
        {
            //Arrange           
            var dh = new DocumentHelper(_testDirectory);
            dh.CreateDirectory();
            var curDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            var logDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day+1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            dh.CreateFile(curDate);

            //Act
            dh.DateCheckAndCreateFile(curDate, curDate);

            //Assert
            Assert.IsFalse(File.Exists($@"{_testDirectory}\Log{logDate.ToString("yyyyMMdd HHmmss fff")}.log"));

        }

        [TestMethod]
        public void DateCheckAndCreateFile_DayIsDifferent_NewfileIsCreated()
        {
            
            //Arrange
            var dh = new DocumentHelper(_testDirectory);
            dh.CreateDirectory();
            var curDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            var logDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            dh.CreateFile(curDate);

            //Act
            dh.DateCheckAndCreateFile(curDate, logDate);

            //Assert
            Assert.IsTrue(File.Exists($@"{_testDirectory}\Log{logDate.ToString("yyyyMMdd HHmmss fff")}.log"));
        }
    }
}
