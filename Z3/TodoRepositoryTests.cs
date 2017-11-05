using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z2;

namespace UnitTestZ2
{
    [TestClass]
    public class TodoRepositoryTests
    {
        public TodoRepository CreateRandom(int size)
        {
            TodoRepository t1 = new TodoRepository();

            Random RNG = new Random();
            string[] values = { "a", "b", "c", "d", "e", "f" };
            string t;

            for (int i = 0; i < size; ++i)
            {
                t = values[RNG.Next(0, 5)] + values[RNG.Next(0, 5)] + values[RNG.Next(0, 5)];

                try
                {
                    var tt = new TodoItem(t);
                    t1.Add(tt);

                    if(i % 2 == 0)
                        t1.MarkAsCompleted(tt.Id);
                }
                catch (DuplicateTodoItemException)
                {
                    --i;
                    continue;
                }

                Thread.Sleep(50);
            }

            return t1;
        }

        [TestMethod]
        public void GetTest()
        {
            TodoRepository t1 = new TodoRepository(), 
                           t2 = new TodoRepository(), 
                           t3 = new TodoRepository();
            TodoItem a = new TodoItem("a"), 
                     b = new TodoItem("b"), 
                     c = new TodoItem("c");

            t1.Add(a);
            t1.Add(b);

            t2.Add(b);
            t2.Add(c);

            t3.Add(c);
            t3.Add(b);
            t3.Add(a);

            Assert.AreEqual(a, t1.Get(a.Id));
            Assert.AreEqual(null, t1.Get(c.Id));

            Assert.AreEqual(b, t2.Get(b.Id));

            Assert.AreEqual(t3.Get(a.Id), t1.Get(a.Id));
            Assert.AreEqual(t3.Get(c.Id), t2.Get(c.Id));
            Assert.AreNotEqual(t3.Get(c.Id), t1.Get(c.Id));
        }

        [TestMethod]
        public void AddTest()
        {
            var t1 = new TodoRepository();
            TodoItem a = new TodoItem("a"),
                b = new TodoItem("b");

            t1.Add(a);
            Assert.ThrowsException<DuplicateTodoItemException>(() => t1.Add(a), "duplicate id: {" + a.Id + "}");

            Assert.AreEqual(b, t1.Add(b));
            Assert.IsFalse(t1.Get(b.Id) == null);
        }

        [TestMethod]
        public void RemoveTest()
        {
            var t1 = new TodoRepository();
            TodoItem a = new TodoItem("a"),
                     b = new TodoItem("b"),
                     c = new TodoItem("c");

            t1.Add(a);
            t1.Add(b);

            Assert.IsTrue(t1.Remove(a.Id));
            Assert.IsTrue(t1.Remove(b.Id));
            Assert.IsFalse(t1.Remove(b.Id));

            t1.Add(a);                          //Prošli test je provjeravao za praznu kolekciju,
            Assert.IsFalse(t1.Remove(c.Id));    //sljedeći provjerava za nepraznu koleciju.
        }

        [TestMethod]
        public void UpdateTest()
        {
            var t1 = new TodoRepository();
            var a = new TodoItem("a");
            var aa = new TodoItem("aa");
            var aaa = new TodoItem("aaa");
            aa.Id = a.Id;

            t1.Add(a);
            Assert.AreEqual("a", t1.Get(a.Id).Text);

            t1.Update(aa);
            Assert.AreEqual("aa", t1.Get(a.Id).Text);
            Assert.AreEqual(1, t1.GetAll().Count);

            t1.Update(aaa);
            Assert.AreEqual(2, t1.GetAll().Count);
        }

        [TestMethod]
        public void CompletedTest()
        {
            var t1 = new TodoRepository();
            TodoItem a = new TodoItem("a"),
                     b = new TodoItem("b"),
                     c = new TodoItem("c");

            t1.Add(a);
            t1.Add(b);
            t1.MarkAsCompleted(b.Id);

            Assert.IsFalse(t1.Get(a.Id).IsCompleted);
            Assert.IsTrue(t1.Get(b.Id).IsCompleted);
            Assert.IsFalse(t1.MarkAsCompleted(c.Id));
        }

        [TestMethod]
        public void GetAllTest()
        {
            TodoRepository t1 = CreateRandom(25),
                           t2 = new TodoRepository();
            var t3 = t1.GetAll();

            Assert.AreEqual(25, t3.Count);

            for(int i = 1; i < 25; ++i)     //Provjera je li dobro sortirano.
                Assert.IsTrue(t3[i].DateCreated.Ticks <= t3[i - 1].DateCreated.Ticks);

            //Razlog zašto testiram za IOOR umjesto ArgumentNulla je zato što će enumerator prije baciti IOOR jer 0. indeks ne postoji.
            Assert.ThrowsException<IndexOutOfRangeException>(() => t2.GetAll());
        }

        [TestMethod]
        public void GetActiveTest()
        {
            TodoRepository t1 = CreateRandom(25),
                           t2 = new TodoRepository();
            var t3 = t1.GetActive();

            Assert.AreEqual(12, t3.Count(x => !x.IsCompleted));
            Assert.ThrowsException<IndexOutOfRangeException>(() => t2.GetActive());

            var tt = new TodoItem("a");
            t2.Add(tt);
            t2.MarkAsCompleted(tt.Id);

            Assert.AreEqual(0, t2.GetActive().Count);
        }

        [TestMethod]
        public void GetCompletedTest()
        {
            TodoRepository t1 = CreateRandom(25),
                           t2 = new TodoRepository();
            var t3 = t1.GetCompleted();

            Assert.AreEqual(13, t3.Count(x => x.IsCompleted));
            Assert.ThrowsException<IndexOutOfRangeException>(() => t2.GetCompleted());

            var tt = new TodoItem("a");
            t2.Add(tt);

            Assert.AreEqual(0, t2.GetCompleted().Count);
        }

        [TestMethod]
        public void GetFilteredTest()
        {
            TodoRepository t1 = CreateRandom(25);
            var t2 = t1.GetActive();
            var t3 = t1.GetCompleted();
            var t4 = t1.GetFiltered(x => !x.IsCompleted);
            var t5 = t1.GetFiltered(x => x.IsCompleted);
            var t6 = t1.GetFiltered(x => x.Text.Contains("a"));

            Assert.IsTrue(t2.SequenceEqual(t4));
            Assert.IsTrue(t3.SequenceEqual(t5));
            Assert.IsFalse(t2.SequenceEqual(t5));

            foreach(TodoItem x in t6)
                Assert.IsTrue(x.Text.Contains("a"));
        }
    }
}