namespace Tasks.Service.Domain.Views;

public class EventCompletionResponse
{
    public Guid? EventId { get; set; }
    public DateTime? OnDate { get; set; }
    public bool? IsComplete { get; set; }
    public DateTime? CreatedOn { get; set; }
}
