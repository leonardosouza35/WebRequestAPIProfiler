using System;
using System.Diagnostics;
using WebRequestAPIProfiler.Log;

namespace WebRequestAPIProfiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Logger.Info("Process Started");        
            WebRequestProfiler.Init();
            stopWatch.Stop();
            Logger.Info($"Total Process Finished in {stopWatch.Elapsed}");

            Console.WriteLine("Press any key to terminate");
            Console.ReadKey();
        }
    }
}
