namespace TodoWebApi.DTOS
{
    /// <summary>
    /// DTO for Creating Todo Item
    /// </summary>
    public class TodoCreateDto
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
        /// Optional DueDate of Todo Item
        /// </summary>
        public DateTime? DueDate { get; set; }
    }
}
