using TodoWebApi.Models;

namespace TodoWebApi.Services
{
    /// <summary>
    /// Interface for Todo Service
    /// </summary>
    public interface ITodoService
    {
        /// <summary>
        /// Get All Todo Items
        /// </summary>
        /// <returns>Returns List of Todo Items</returns>
        List<TodoItem> GetAll();
        /// <summary>
        /// Gets Todo items by Search string which searches in Title and Description
        /// </summary>
        /// <param name="criteria">Search string</param>
        /// <returns>Returns List of Mathing Todo Items or empty List if no matches found.</returns>
        List<TodoItem> GetByCriteria(string criteria);

        /// <summary>
        /// Get TodoItem by id. Null returned if no mathcing item found.
        /// </summary>
        /// <param name="id">Id of todo Item</param>
        /// <returns>Returns Todo Item if found or null</returns>
        TodoItem? GetItemById(int id);        
        /// <summary>
        /// Add a Todo Item
        /// </summary>
        /// <param name="item">Todo Item object</param>
        /// <returns>true if successful otherwise false</returns>
        bool AddTodoItem(TodoItem item);
        /// <summary>
        /// Update Todo Item
        /// </summary>
        /// <param name="item">Todo Item object</param>
        /// <returns>true if successful otherwise false</returns>
        bool UpdateTodoItem(TodoItem item);
        /// <summary>
        /// Delete Todo Item
        /// </summary>
        /// <param name="id">Id of Todo Item</param>
        /// <returns>true if successful otherwise false</returns>
        bool DeleteTodoItem(int id);

    }
}
