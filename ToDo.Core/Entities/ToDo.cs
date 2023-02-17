namespace ToDo.Core.Entities;

public partial class ToDo
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public byte Status { get; set; }

    public DateTime? DueDate { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
