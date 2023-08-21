namespace Tasks.Service.Domain.Models;

public class ChecklistLabel
{
    public Guid? ChecklistId { get; set; }
    public Guid? LabelId { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
}
