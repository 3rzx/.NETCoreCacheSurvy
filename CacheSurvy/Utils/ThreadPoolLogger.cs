using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CacheSurvy.Utils
{
    public struct ThreadPoolUsageStats
    {
        public int BusyIoThreads { get; set; }

        public int MinIoThreads { get; set; }

        public int MaxIoThreads { get; set; }

        public int BusyWorkerThreads { get; set; }

        public int MinWorkerThreads { get; set; }

        public int MaxWorkerThreads { get; set; }
    }

    internal class ThreadPoolLogger : IDisposable
    {
        private TimeSpan logFrequency;
        private bool disposed;

        public ThreadPoolLogger(TimeSpan logFrequency)
        {
            if (logFrequency <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException("logFrequency");
            }

            this.logFrequency = logFrequency;
            this.StartLogging();
        }

        public static ThreadPoolUsageStats GetThreadPoolStats()
        {
            /* BusyThreads =  TP.GetMaxThreads() –TP.GetAVailable();
             If BusyThreads >= TP.GetMinThreads(), then threadpool growth throttling is possible.*/

            int maxIoThreads, maxWorkerThreads;
            ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxIoThreads);

            int freeIoThreads, freeWorkerThreads;
            ThreadPool.GetAvailableThreads(out freeWorkerThreads, out freeIoThreads);

            int minIoThreads, minWorkerThreads;
            ThreadPool.GetMinThreads(out minWorkerThreads, out minIoThreads);

            int busyIoThreads = maxIoThreads - freeIoThreads;
            int busyWorkerThreads = maxWorkerThreads - freeWorkerThreads;

            return new ThreadPoolUsageStats
            {
                BusyIoThreads = busyIoThreads,
                MinIoThreads = minIoThreads,
                MaxIoThreads = maxIoThreads,
                BusyWorkerThreads = busyWorkerThreads,
                MinWorkerThreads = minWorkerThreads,
                MaxWorkerThreads = maxWorkerThreads,
            };
        }

        public void Dispose()
        {
            this.disposed = true;
        }

        protected virtual void LogUsage(ThreadPoolUsageStats stats)
        {
            string message = string.Format(
                "[{0}] IOCP:(Busy={1},Min={2},Max={3}), WORKER:(Busy={4},Min={5},Max={6})",
                DateTimeOffset.UtcNow.ToString("u"),
                stats.BusyIoThreads,
                stats.MinIoThreads,
                stats.MaxIoThreads,
                stats.BusyWorkerThreads,
                stats.MinWorkerThreads,
                stats.MaxWorkerThreads);

            Console.WriteLine(message);
        }

        private async void StartLogging()
        {
            try
            {
                while (!this.disposed)
                {
                    await Task.Delay(this.logFrequency);

                    var stats = GetThreadPoolStats();

                    this.LogUsage(stats);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
