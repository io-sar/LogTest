using System;
using LogTest.Models;


namespace LogUsers
{
    using System.Threading;

    using LogTest;

    public class Program
    {
        private static string _loggerDirectory = @"C:\LogTest";


        static void Main(string[] args)
        {           
           
            var logger = new Logger(_loggerDirectory);
            var logger2 = new Logger(_loggerDirectory);

            logger.StartLogger();            
            logger2.StartLogger();
         
            for (int i = 0; i < 15; i++)
            {
                logger.Write("Number with Flush: " + i.ToString());
                Thread.Sleep(50);
            }

            logger.StopWithFlush();

            for (int i = 50; i > 0; i--)
            {                                      
                logger2.Write("Number with No flush: " + i.ToString());
                Thread.Sleep(50);
            }

           logger2.StopWithoutFlush();

            Console.ReadLine();
        }
    }
}
