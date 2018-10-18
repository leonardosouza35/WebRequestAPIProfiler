using log4net;
using System;
using System.IO;

namespace WebRequestAPIProfiler.Log
{
    public class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        public static void Info(string message)
        {
            System.Console.WriteLine(message);            
            log.Info(message);
        }

        public static void Error(string message)
        {
            System.Console.WriteLine(message);
            log.Error(message);
        }

        public static void Warn(string message)
        {
            System.Console.WriteLine(message);
            log.Warn(message);
        }

    }
}
