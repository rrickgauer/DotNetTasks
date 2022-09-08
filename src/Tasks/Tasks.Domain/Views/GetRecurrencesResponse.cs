using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tasks.CustomAttributes;
using Tasks.Domain.Models;

namespace Tasks.Domain.Views;

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
