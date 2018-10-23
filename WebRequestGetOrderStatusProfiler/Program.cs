using System;
using System.Diagnostics;
using WebRequestLogger.Log;

namespace WebRequestGetOrderStatusProfiler
{
    class Program
    {
        static void Main(string[] args)
        {          
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Logger.Info("==================Process Started==================");
            WebRequestGetOrderStatus.Init(RunModeEnum.Parallel);
            stopWatch.Stop();
            Logger.Info($"==================Total Process Finished in {stopWatch.Elapsed}==================");

            Console.ReadKey();
        }

       
    }
}
