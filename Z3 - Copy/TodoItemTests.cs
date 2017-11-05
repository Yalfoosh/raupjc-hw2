using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z2;

namespace UnitTestZ2
{
    [TestClass]
    public class TodoItemTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var t1 = new TodoItem("Test 1.");

            //Moram uspavati dretvu jer je stvaranje dovoljno brz u tickovima da datum bude jednak.
            Thread.Sleep(1000);
            var t2 = new TodoItem("Test 2.");

            //Ovo bi "trebalo" uvijek vrijediti. U svakom slučaju ne bi trebalo pasti svakog puta.
            Assert.AreNotSame(t1.Id, t2.Id);
            Assert.AreNotSame(t1.Text, t2.Text);
            Assert.AreNotEqual(t1.DateCreated.CompareTo(t2.DateCreated), 0);
        }

        [TestMethod]
        public void IsCompletedTest()
        {
            var t1 = new TodoItem("Test 1.");
            var t2 = new TodoItem("Test 2.");

            t1.Complete();

            Assert.IsTrue(t1.IsCompleted);
            Assert.IsFalse(t2.IsCompleted);
        }

        [TestMethod]
        public void EqualsTest()
        {
            var t1 = new TodoItem("Test 1.");
            var t2 = new TodoItem("Test 2.");

            var t3 = t1;
            var t1_1 = t1.Id;

            Assert.IsTrue(t1.Equals(t3));
            Assert.IsFalse(t1.Equals(t2));
            Assert.IsFalse(t1.Equals(t1_1));
        }

        [TestMethod]
        public void HashCodeTest()
        {
            var t1 = new TodoItem("Test 1.");
            var t2 = new TodoItem("Test 1.");

            var t3 = t1;

            Assert.IsTrue(t1.GetHashCode() == t3.GetHashCode());
            Assert.IsFalse(t1.GetHashCode() == t2.GetHashCode());
            Assert.IsFalse(t2.GetHashCode() == t3.GetHashCode());
        }

        [TestMethod]
        public void OperatorTest()
        {
            var t1 = new TodoItem("Test 1.");
            var t2 = new TodoItem("Test 1.");
            var t3 = new TodoItem("Test 2.");
            var t4 = new TodoItem("Test 2.");

            t4.Complete();
            var t5 = t1;

            Assert.IsTrue(t1 == t5);
            Assert.IsFalse(t1 == t2);
            Assert.IsFalse(t2 == t3);
            Assert.IsFalse(t3 == t4);
            Assert.IsFalse(t2 == t5);

            //Ne trebam kontrolirati operator != jer je on obrat ==, kao što je vidljivo i u kodu:
            //ako gornjih 5 testova prođu, proći će i za operator !=.
        }
    }
}