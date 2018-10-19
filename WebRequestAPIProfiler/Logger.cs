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
            log.Info(message);
        }

        public static void Error(string message)
        {            
            log.Error(message);
        }

        public static void Warn(string message)
        {            
            log.Warn(message);
        }

    }
}
