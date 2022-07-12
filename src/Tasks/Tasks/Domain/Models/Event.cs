using System.Text.Json.Serialization;
using Tasks.CustomAttributes;
using Tasks.Domain.Enums;
using Tasks.Mappers;

namespace Tasks.Domain.Models
{
	/// <summary>
	/// Event domain model
	/// </summary>
    public class Event
    {
		[SqlColumn("id")] public Guid? Id { get; set; }
		[SqlColumn("user_id")] public Guid? UserId { get; set; }
		[SqlColumn("name")] public string? Name { get; set; }
		[SqlColumn("description")] public string? Description { get; set; }
		[SqlColumn("phone_number")] public string? PhoneNumber { get; set; }
		[SqlColumn("location")] public string? Location { get; set; }
		[SqlColumn("starts_on")] public DateTime? StartsOn { get; set; }
		[SqlColumn("ends_on")] public DateTime? EndsOn { get; set; }
		[SqlColumn("starts_at")] public TimeSpan? StartsAt { get; set; }
		[SqlColumn("ends_at")] public TimeSpan? EndsAt { get; set; }
		[SqlColumn("frequency")] public Frequency? Frequency { get; set; }
		[SqlColumn("separation")] public uint? Separation { get; set; }
		[SqlColumn("created_on")] public DateTime? CreatedOn { get; set; }
		[SqlColumn("recurrence_day")] public int? RecurrenceDay { get; set; }
		[SqlColumn("recurrence_week")] public int? RecurrenceWeek { get; set; }
		[SqlColumn("recurrence_month")] public int? RecurrenceMonth { get; set; }
	}
}
