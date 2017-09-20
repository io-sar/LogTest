using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using LogTest.Models;
using LogTest.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ThreadState = System.Threading.ThreadState;

namespace LogComponentTests.Services
{
    [TestClass]
    public class ThreadServiceTests
    {


        //NOTE : I lack of multithreading unit test experience. I assume that if I have a thread running on the background and try to block it with my logger thread.
        //Then check if my test thread remains idle

        [TestMethod]
        public void CreateBackgroundThread_CreateThread_CheckIfItRuns()
        {
            // Arrange
            var logger = new Mock<Logger>();
            var ts = new ThreadService(logger.Object);

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;           
                }).Start();

            // Act
            ts.CreateBackgroundThread();
            

            // Assert
           Assert.IsTrue((Thread.CurrentThread.ThreadState & ThreadState.WaitSleepJoin) != 0);
        }

    }
}
