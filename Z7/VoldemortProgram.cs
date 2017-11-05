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

        private static async Task<int> GetTheMagicNumber()
        {
            return await IKnowIGuyWhoKnowsAGuy();
        }

        private static async Task<int> IKnowIGuyWhoKnowsAGuy()
        {
            return await IKnowWhoKnowsThis(10) + await IKnowWhoKnowsThis(5);
        }

        private static async Task<int> IKnowWhoKnowsThis(int n)
        {
            return await Factorials.FactorialDigitSum(n);
        }

        // Ignore this part .
        static void Main(string[] args)
        {
            // Main method is the only method that
            // can’t be marked with async.
            // What we are doing here is just a way for us to simulate
            // async - friendly environment you usually have with
            // other. NET application types (like web apps, win apps etc.)
            // Ignore main method, you can just focus on LetsSayUserClickedAButtonOnGuiMethod() as a
            // first method in the call hierarchy.

            Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();
        }
    }
}