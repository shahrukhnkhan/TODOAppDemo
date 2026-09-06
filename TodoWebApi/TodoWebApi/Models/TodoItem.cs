namespace TodoWebApi.Models
{
    /// <summary>
    /// Todo Item Class for holding details
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Unique Id for TodoItem
        /// </summary>
        public int Id { get; set; } = 0;
        /// <summary>
        /// Title of Todo Item. 
        /// Default value is empty.
        /// </summary>
        public string Title { get; set; }  = string.Empty;
        /// <summary>
        /// Description of Todo Item
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Status of Todo Item
        /// </summary>
        public bool IsComplete { get; set; } = false;
        /// <summary>
        /// Created by User Name
        /// </summary>
        public DateTime CreationDate { get; set; } = DateTime.Now;
        /// <summary>
        /// Due Date of Todo Item
        /// </summary>
        public DateTime? DueDate { get; set; }

    }
}
