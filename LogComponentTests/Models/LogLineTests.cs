using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogTest.Models;

namespace LogComponentTests.Models
{
    [TestClass]
    public class LogLineTests
    {


        [TestMethod]
        public void LogLine_NewInstance_TextIsEmpty()
        {
            // Arrange
            var logLine = new LogLine();
            // Act

            // Assert
            logLine.Text.Should().BeEmpty();
        }

        [TestMethod]
        public void LogLine_SetText_TextIsSet()
        {
            // Arrange
            var logLine =  new LogLine();

            // Act
            logLine.Text = "Abc";

            // Assert
            logLine.Text.ShouldBeEquivalentTo("Abc", "its being set");
        }

        [TestMethod]
        public void LogLine_SetText_LineTextIsFormattedAsExpected()
        {
            // Arrange
            const string test = "UnitTesting";
            var expected = $"{test}. ";
            var logLine = new LogLine();

            // Act
            logLine.Text = test;

            // Assert
            logLine.LineText().ShouldBeEquivalentTo(expected);
        }
    }
}

