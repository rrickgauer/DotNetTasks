using Tasks.CustomAttributes;

namespace Tasks.Domain.Models
{
    /// <summary>
    /// Recurrence domain model
    /// </summary>
    public class Recurrence
    {
        [SqlColumn("event_id")]
        public Guid? EventId { get; set; }

        [SqlColumn("name")]
        public string? Name { get; set; }

        [SqlColumn("occurs_on")]
        public DateTime? OccursOn { get; set; }

        [SqlColumn("starts_at")]
        public TimeSpan? StartsAt { get; set; }

        [SqlColumn("completed")]
        public bool? Completed { get; set; }
    }
}
