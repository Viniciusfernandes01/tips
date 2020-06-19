using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Tips
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            var result = PrimesInRange(200, 800000);
            sw.Stop();
            Console.WriteLine(
                $"{result} prime numbers found in {sw.ElapsedMilliseconds / 1000} seconds " +
                $"({Environment.ProcessorCount} processors).");
        }


        public static long PrimesInRange(long start, long end)
        {
            long result = 0;
            Parallel.For(start, end, number =>
            {
                if (IsPrime(number))
                {
                    Interlocked.Increment(ref result);
                }
            });

            return result;
        }

        static bool IsPrime(long number)
        {
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            for (long divisor = 3; divisor < (number / 2); divisor += 2)
            {
                if (number % divisor == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
