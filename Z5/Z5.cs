using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Z5
{
    public class Z5
    {
        public static void LongOperation(string taskName)
        {
            Thread.Sleep(1000);

            Console.WriteLine("{0} Finished . Executing Thread : {1}",
                taskName,
                Thread.CurrentThread.ManagedThreadId);
        }

        public static void Serial()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            LongOperation("A");
            LongOperation("B");
            LongOperation("C");
            LongOperation("D");
            LongOperation("E");

            stopwatch.Stop();

            Console.WriteLine("Synchronous long operation calls finished {0} sec.\n", stopwatch.Elapsed.TotalSeconds);
        }

        public static void Paralleled()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Parallel.Invoke(() => LongOperation("A"),
                () => LongOperation("B"),
                () => LongOperation("C"),
                () => LongOperation("D"),
                () => LongOperation("E"));

            stopwatch.Stop();

            Console.WriteLine("Parallel long operation calls finished {0} sec.\n",
                stopwatch.Elapsed.TotalSeconds);
        }

        public static void SyncAsync()
        {
            List<int> results = new List<int>();

            try
            {
                Parallel.For(0, 100000, i =>
                {
                    Thread.Sleep(1);
                    results.Add(i * i);
                });
            }
            catch (Exception)
            {
                // Ignoriram, samo da ne ruši.
            }

            Console.WriteLine("Bag length should be 100000. Length is {0}\n",
                              results.Count);
        }

        public static void TrueAsync()
        {
            ConcurrentBag<int> iterations = new ConcurrentBag<int>();

            Parallel.For(0, 100000, i =>
            {
                Thread.Sleep(1);
                iterations.Add(i);
            });

            Console.WriteLine("Bag length should be 100000. Length is {0}\n",
                              iterations.Count);
        }

        public static void Main(string[] args)
        {
            Serial();
            Paralleled();
            SyncAsync();
            TrueAsync();

            Console.ReadLine();
        }
    }
}