using System;
using System.Threading.Tasks;
using Z6;

namespace Z7
{
    class VoldemortProgram
    {
        private static async void LetsSayUserClickedAButtonOnGuiMethod()
        {
            var result = await GetTheMagicNumber();
            Console.WriteLine(result);
        }

        private static async Task<int> GetTheMagicNumber() => await IKnowIGuyWhoKnowsAGuy();
        private static async Task<int> IKnowIGuyWhoKnowsAGuy() => await IKnowWhoKnowsThis(10) + await IKnowWhoKnowsThis(5);
        private static async Task<int> IKnowWhoKnowsThis(int n) => await Factorials.FactorialDigitSum(n);

        static void Main()
        {
            Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();
        }
    }
}