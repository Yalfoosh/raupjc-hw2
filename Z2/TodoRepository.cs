using System;
using System.Collections.Generic;
using System.Linq;
using GenericListEnumerator;

namespace Z2
{
    /// <summary>
    /// Class that encapsulates all the logic for accessing TodoTtems .
    /// </summary>
    public class TodoRepository : ITodoRepository
    {
        /// <summary>
        /// Repository does not fetch todoItems from the actual database,
        /// it uses in memory storage for this excersise.
        /// </summary>
        private readonly IGenericList<TodoItem> _inMemoryTodoDatabase;

        public TodoRepository(IGenericList<TodoItem> initialDbState = null)
        {
            _inMemoryTodoDatabase = initialDbState ?? new GenericList<TodoItem>();
        }

        public TodoItem Get(Guid todoId)
        {
            try
            {
                return _inMemoryTodoDatabase.FirstOrDefault(x => x.Id.Equals(todoId));
            }
            catch (IndexOutOfRangeException e)
            {
                return null;
            }
        }

        public TodoItem Add(TodoItem todoItem)
        {
            if(_inMemoryTodoDatabase.Contains(todoItem))
                throw new DuplicateTodoItemException("duplicate id: {" + todoItem.Id + "}");

            _inMemoryTodoDatabase.Add(todoItem);
            return todoItem;
        }

        public bool Remove(Guid todoId)
        {
            var sought4 = Get(todoId);

            return sought4 != null && _inMemoryTodoDatabase.Remove(sought4);
        }

        public TodoItem Update(TodoItem todoItem)   //Ne mogu dobiti direktno polje s referencama, pa moram brisati-pisati.
        {                                           //Moglo bi se riješiti unsafe metodom i pointerima, ali to zahtjeva drukčiju implementaciju u DZ1.
            var sought4 = Get(todoItem.Id);

            if (sought4 != null)
            {
                sought4.Text = todoItem.Text;
                sought4.DateCreated = todoItem.DateCreated;
                sought4.DateCompleted = todoItem.DateCompleted;

                return sought4;
            }

            return Add(todoItem);
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            var sought4 = Get(todoId);

            return sought4 != null && sought4.Complete();
        }

        public List<TodoItem> GetAll() => _inMemoryTodoDatabase.OrderByDescending(x => x.DateCreated).ToList();

        public List<TodoItem> GetActive() => _inMemoryTodoDatabase.Where(x => !x.IsCompleted).ToList();
        public List<TodoItem> GetCompleted() => _inMemoryTodoDatabase.Where(x => x.IsCompleted).ToList();

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction) => _inMemoryTodoDatabase.Where(filterFunction).ToList();
    }
}