using Microsoft.AspNetCore.Mvc;
using System.Net;
using TodoWebApi.DTOS;
using TodoWebApi.Models;
using TodoWebApi.Services;

namespace TodoWebApi.Controllers
{
    /// <summary>
    /// Controller to Handle Todo Items
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        /// <summary>
        /// Todo Service Object to be used for handling todo items requests
        /// </summary>
        private readonly ITodoService _todoService;

        /// <summary>
        /// Constructor for Todo Controller
        /// </summary>
        /// <param name="todoService">Todo Service object</param>
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }
        /// <summary>
        /// Gets All Todo Items
        /// </summary>
        /// <returns>Returns all todo items</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return new JsonResult(new {data = _todoService.GetAll(), status = HttpStatusCode.OK});
        }

        /// <summary>
        /// Get Todo Item by Id
        /// </summary>
        /// <param name="id">id of Todo Item to fetch</param>
        /// <returns>return matching Todo Item in response or null value if no matches found</returns>
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var selItem = _todoService.GetItemById(id);
            return new JsonResult(new { data = selItem, status = HttpStatusCode.OK });
        }

        /// <summary>
        /// Add Todo Item Method
        /// </summary>
        /// <param name="item">Todo Item object for creation</param>
        /// <returns>true or false in the response object</returns>
        [HttpPost]
        public IActionResult AddTodoItem(TodoCreateDto item)
        {
            if (string.IsNullOrEmpty(item.Title) || string.IsNullOrEmpty(item.Description))
            {
                return BadRequest("Title, Description are Required");
            }
 
            var selItem = _todoService.AddTodoItem(new TodoItem { Title=item.Title,Description=item.Description, DueDate=item.DueDate });
            return new JsonResult(new { data = selItem, status = HttpStatusCode.OK });
        }
        /// <summary>
        /// Update Todo Item Method
        /// </summary>
        /// <param name="todoid">id of the Todo Item</param>
        /// <param name="item">Todo item object for Update</param>
        /// <returns>true or false in the response object</returns>
        [HttpPut("{todoid:int}")]
        public IActionResult UpdateTodoItem(int todoid,TodoUpdateDto item)
        {
            var chkItem = _todoService.GetItemById(todoid);
            if (chkItem == null)
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(item.Title) || string.IsNullOrEmpty(item.Description))
            {
                return BadRequest("Both Title and Description are Required");
            }

            chkItem.Title = item.Title;
            chkItem.Description = item.Description;
            chkItem.IsComplete = item.IsComplete;
            chkItem.DueDate = item.DueDate;

            var selItem = _todoService.UpdateTodoItem(chkItem);
            return new JsonResult(new { data = selItem, status = HttpStatusCode.OK });
        }
        /// <summary>
        /// Patch Method for Todo Item to update particular field/s of Todo Item
        /// </summary>
        /// <param name="todoid">id of Todo Item</param>
        /// <param name="item">Todo item object for Patching</param>
        /// <returns>true or false in response object</returns>
        [HttpPatch("{todoid:int}")]
        public IActionResult UpdateTodoItem(int todoid, TodoPatchDto item)
        {
            var chkItem = _todoService.GetItemById(todoid);
            if (chkItem == null)
            {
                return NotFound();
            }

            if ((item.Title != null && string.IsNullOrEmpty(item.Title))
                || (item.Description != null && string.IsNullOrEmpty(item.Description)))
            {
                return BadRequest("Both Title and Description are Required");
            }

            if (item.Title != null)
                chkItem.Title = item.Title;

            if (item.Description != null)
                chkItem.Description = item.Description;

            if (item.DueDate.HasValue)
                chkItem.DueDate = item.DueDate.Value;

            if (item.IsComplete.HasValue)
                chkItem.IsComplete = item.IsComplete.Value;


            var selItem = _todoService.UpdateTodoItem(chkItem);
            return new JsonResult(new { data = selItem, status = HttpStatusCode.OK });
        }

        /// <summary>
        /// Delete method for Todo Item
        /// </summary>
        /// <param name="id">id of Todo Item to delete</param>
        /// <returns>true or false in response object</returns>
        [HttpDelete("{id:int}")]
        public IActionResult DeleteTodoItem(int id)
        {
            var chkItem = _todoService.GetItemById(id);
            if (chkItem == null)
            {
                return NotFound();
            }

            var selItem = _todoService.DeleteTodoItem(id);
            return new JsonResult(new { data = selItem, status = HttpStatusCode.OK });
        }
    }
}
