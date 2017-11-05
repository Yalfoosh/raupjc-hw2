using System.Threading.Tasks;

namespace Z6
{
    public class Factorials
    {
        public static async Task<int> FactorialDigitSum(int n)
        {
            ulong t = 1;
            int toRet = 0;

            for (int i = n; i > 1; --i)
                t *= (ulong)i;

            for (; t > 0; t /= 10)
                toRet += (int)(t % 10);

            return toRet;
        }
    }
}