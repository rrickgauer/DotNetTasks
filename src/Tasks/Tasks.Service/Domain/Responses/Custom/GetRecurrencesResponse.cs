using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.Models;

namespace Tasks.Service.Domain.Responses.Custom;

public class GetRecurrencesResponse
{
    public Event? Event { get; set; }

    public IEnumerable<Label> Labels { get; set; } = Enumerable.Empty<Label>();

    [SqlColumn("occurs_on")]
    public DateTime? OccursOn { get; set; }

    [SqlColumn("completed")]
    public bool? Completed { get; set; }

    [SqlColumn("cancelled")]
    public bool? Cancelled { get; set; }
}
