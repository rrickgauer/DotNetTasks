namespace Tasks.Service.Domain.Models;

public class Label
{
    public Guid? Id { get; set; }
    public Guid? UserId { get; set; }
    public string? Name { get; set; }
    public string? Color { get; set; }
    public DateTime? CreatedOn { get; set; }
}
