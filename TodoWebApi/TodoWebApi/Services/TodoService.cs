using TodoWebApi.Models;

namespace TodoWebApi.Services
{
    /// <summary>
    /// Implemetation of Todo Service Interface
    /// </summary>
    public class TodoService : ITodoService
    {
        /// <summary>
        /// In Memory list of Todo Items
        /// </summary>
        private readonly List<TodoItem> _todoItems = new List<TodoItem>();

        /// <summary>
        /// Add a Todo Item
        /// </summary>
        /// <param name="item">Todo Item object</param>
        /// <returns>true if successful otherwise false</returns>
        public bool AddTodoItem(TodoItem item)
        {

            try
            {
                int maxId = _todoItems.Any() ? _todoItems.Max(x => x.Id) : 0;
                item.Id = maxId + 1;
                _todoItems.Add(item);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Delete Todo Item
        /// </summary>
        /// <param name="id">Id of Todo Item</param>
        /// <returns>true if successful otherwise false</returns>
        public bool DeleteTodoItem(int id)
        {

            var todoItem = GetItemById(id);
            if (todoItem != null)
            {
                _todoItems.Remove(todoItem);
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Get All Todo Items
        /// </summary>
        /// <returns>Returns List of Todo Items</returns>
        public List<TodoItem> GetAll()
        {
            return _todoItems;
        }
        /// <summary>
        /// Gets Todo items by Search string which searches in Title and Description
        /// </summary>
        /// <param name="criteria">Search string</param>
        /// <returns>Returns List of Mathing Todo Items or empty List if no matches found.</returns>

        public List<TodoItem> GetByCriteria(string criteria)
        {
            return _todoItems.Where(x => x.Title.Contains(criteria) || x.Description.Contains(criteria)).ToList();
        }
        /// <summary>
        /// Get TodoItem by id. Null returned if no mathcing item found.
        /// </summary>
        /// <param name="id">Id of todo Item</param>
        /// <returns>Returns Todo Item if found or null</returns>
        public TodoItem? GetItemById(int id)
        {
            return _todoItems.FirstOrDefault(x => x.Id == id);
        }
        /// <summary>
        /// Update Todo Item
        /// </summary>
        /// <param name="item">Todo Item object</param>
        /// <returns>true if successful otherwise false</returns>
        public bool UpdateTodoItem(TodoItem item)
        {
            var todoItem = GetItemById(item.Id);
            if (todoItem != null)
            {
                todoItem.Title= item.Title;
                todoItem.Description= item.Description;
                todoItem.DueDate= item.DueDate;
                todoItem.IsComplete = item.IsComplete;

                return true;
            }
            else
                return false;
        }
    }
}
