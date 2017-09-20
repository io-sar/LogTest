using System;
using System.Runtime.CompilerServices;
using FluentAssertions;
using LogComponentTests.Helpers;
using LogTest.Helpers;
using LogTest.Models;
using LogTest.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LogComponentTests.Services
{
    [TestClass]
    public class DocumentServiceTests
    {
        private string _testDirectory = @"C:\LogTestTests\DocumentServicesTests";

        [TestMethod]
        public void AddLogLine_LogLineIsNull_ExceptionIsHandled()
        {
            // Arrange
            
            var ds = new DocumentService(_testDirectory);
            ds.CreateLogFile();
            
            LogLine logLine = null;
    
            // Act
            Action act = () => ds.AddLogLine(logLine);

            // Assert
            act.ShouldNotThrow();
        }

        [TestMethod]
        public void AddLogLine_LogLineIsSet_DoesntThrowExceptions()
        {
            // Arrange
            var ds = new DocumentService(_testDirectory);
            ds.CreateLogFile();
            LogLine logLine = new LogLine();

            // Act
            Action act = () => ds.AddLogLine(logLine);

            // Assert
            act.ShouldNotThrow();
        }
    }
}
