using System.Linq;
using Z1;

namespace Z4
{
    public class HomeworkLinqQueries
    {
        public static string[] Linq1(int[] intArray)
        {
            return intArray.GroupBy(x => x)
                           .OrderBy(x => x.Key)
                           .Select(x => "Broj " + x.Key + " ponavlja se " + x.Count() + " puta")
                           .ToArray();
        }

        public static University[] Linq2_1(University[] universityArray)
        {
            return universityArray.Where(x => x.Students.All(y => y.Gender.Equals(Gender.Male)))
                                  .ToArray();
        }

        public static University[] Linq2_2(University[] universityArray)
        {
            return universityArray.Where(y => y.Students.Length < universityArray.Average(x => x.Students.Length))
                                  .ToArray();
        }

        public static Student[] Linq2_3(University[] universityArray)
        {
            return universityArray.SelectMany(x => x.Students)
                                  .Distinct()
                                  .ToArray();
        }

        public static Student[] Linq2_4(University[] universityArray)
        {
            return universityArray.Where(x => x.Students.All(y => y.Gender.Equals(x.Students.First().Gender)))
                                  .SelectMany(y => y.Students)
                                  .Distinct()
                                  .ToArray();
        }

        public static Student[] Linq2_5(University[] universityArray)
        {
            return universityArray.SelectMany(x => x.Students)
                                  .GroupBy(x => x)
                                  .Where(x => x.Count() > 1)
                                  .Select(x => x.Key)
                                  .ToArray();
        }
    }
}