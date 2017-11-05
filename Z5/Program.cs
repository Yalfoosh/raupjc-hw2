using System;
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

            Console.WriteLine("Synchronous long operation calls finished {0} sec.", stopwatch.Elapsed.TotalSeconds);
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

            Console.WriteLine("Parallel long operation calls finished {0} sec.",
                stopwatch.Elapsed.TotalSeconds);
        }

        public static void Main(string[] args)
        {
            Serial();
            Paralleled();

            System.Console.ReadLine();
        }
    }
}