namespace TodoWebApi.DTOS
{
    /// <summary>
    /// DTO for Patching Todo Item
    /// </summary>
    public class TodoPatchDto
    {
        /// <summary>
        /// Title of Todo Item
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Description of Todo Item
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Completion Status of Todo Item
        /// </summary>
        public bool? IsComplete { get; set; }
        /// <summary>
        /// DueDate of Todo Item
        /// </summary>
        public DateTime? DueDate { get; set; }

    }
}
