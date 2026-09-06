namespace TodoWebApi.DTOS
{
    /// <summary>
    /// DTO Object for Updating Todo
    /// </summary>
    public class TodoUpdateDto
    {
        /// <summary>
        /// Title of Todo Item
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Description of Todo Item
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Completion Status of Todo Item
        /// </summary>
        public bool IsComplete { get; set; } = false;
        /// <summary>
        /// Optional DueDate of Todo Item
        /// </summary>
        public DateTime? DueDate { get; set; }
    }
}
