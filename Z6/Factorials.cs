using System;
using System.Threading.Tasks;

namespace Z6
{
    public class Factorials
    {
        public static int FDSum(int n)
        {
            if (n > 0)
            {
                ulong t = 1;
                int toRet = 0;

                for (int i = n; i > 1; --i)
                    t *= (ulong)i;

                for (; t > 0; t /= 10)
                    toRet += (int)(t % 10);

                return toRet;
            }

            throw new NotFiniteNumberException("The factorial of a negative integer is divergent.");
        }

        public static Task<int> FactorialDigitSum(int n) => Task.Run(() => FDSum(n));
    }
}