namespace ToDo.WebAPI.DTOs
{
    public class ToDoDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public byte Status  { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
