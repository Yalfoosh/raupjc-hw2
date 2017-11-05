using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z1;

namespace UnitTestZ1
{
    [TestClass]
    public class StudentUnitTest
    {
        [TestMethod]
        public void Test1()
        {
            var topStudents = new List<Student>()
            {
                new Student ("Ivan", jmbag :"001234567") ,
                new Student ("Luka", jmbag :"3274272") ,
                new Student ("Ana", jmbag :"9382832")
            };

            var ivan = new Student("Ivan", jmbag: "001234567");

            //Trebalo bi biti true, no hoće li?
            Assert.IsTrue(topStudents.Contains(ivan));
        }

        [TestMethod]
        public void Test2()
        {
            var list = new List<Student>()
            {
                new Student ("Ivan", jmbag :"001234567") ,
                new Student ("Ivan", jmbag :"001234567")
            };

            //Trebalo bi biti 1 jer imamo 2 ista studenta, tj. samo jednog jedinstvenog.
            Assert.AreEqual(1, list.Distinct().Count());
        }

        [TestMethod]
        public void Test3()
        {
            var topStudents = new List<Student>()
            {
                new Student ("Ivan", jmbag :"001234567") ,
                new Student ("Luka", jmbag :"3274272") ,
                new Student ("Ana", jmbag :"9382832")
            };

            var ivan = new Student("Ivan", jmbag: "001234567");

            //Trebalo bi biti true jer znamo da smo definirali studenta s imenom Ivan.
            Assert.IsTrue(topStudents.Any(s => s == ivan));
        }
    }
}
