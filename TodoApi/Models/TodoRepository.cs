using System.Collections.Concurrent;

namespace TodoApi.Models
{
    public class TodoRepository : ITodoRepository
    {
        private static ConcurrentDictionary<string, TodoItem> _todos =
              new ConcurrentDictionary<string, TodoItem>();

        public TodoRepository()
        {
            Add(new TodoItem { Name = "Item1" });
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _todos.Values;
        }

        public void Add(TodoItem item)
        {
            item.Key = Guid.NewGuid().ToString();
            _todos[item.Key] = item;
        }

        public TodoItem Find(string key)
        {
            TodoItem item;
            _todos.TryGetValue(key, out item);
            return item;
        }

        public TodoItem Remove(string key)
        {
            TodoItem item;
            _todos.TryRemove(key, out item);
            return item;
        }

        
        public void Update(TodoItem item, bool isFullUpdate)
        {
            if (isFullUpdate)  // PUT.
            {
                _todos[item.Key] = item;
            }
            else  // PATCH.
            {
                if (item.Name is not null)
                    _todos[item.Key].Name = item.Name;

                if (item.IsComplete)
                    _todos[item.Key].IsComplete = true;
            }  
        }
    }
}
