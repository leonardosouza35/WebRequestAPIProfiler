using log4net;
using System;
using System.IO;
using System.Reflection;

namespace WebRequestLogger.Log
{
    public class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
