using System.Text.Json.Serialization;
using Tasks.Domain.Enums;

namespace Tasks.Domain.Models
{
	/// <summary>
	/// Event domain model
	/// </summary>
    public class Event
    {
		[JsonIgnore] public Guid? Id { get; set; }
		[JsonIgnore] public string? UserId { get; set; }
		[JsonIgnore] public string? Name { get; set; }
		[JsonIgnore] public string? Description { get; set; }
		[JsonIgnore] public string? PhoneNumber { get; set; }
		[JsonIgnore] public string? Location { get; set; }
		[JsonIgnore] public DateTime? StartsOn { get; set; }
		[JsonIgnore] public DateTime? EndsOn { get; set; }
		[JsonIgnore] public TimeSpan? StartsAt { get; set; }
		[JsonIgnore] public TimeSpan? EndsAt { get; set; }
		[JsonIgnore] public Frequency? Frequency { get; set; }
		[JsonIgnore] public uint? Separation { get; set; }
		[JsonIgnore] public TimeSpan? CreatedOn { get; set; }
		[JsonIgnore] public int? RecurrenceDay { get; set; }
		[JsonIgnore] public int? RecurrenceWeek { get; set; }
		[JsonIgnore] public int? RecurrenceMonth { get; set; }
    }
    

    public class EventDto : Event
    {
        public new Guid? Id { get; set; }
        //public new string? UserId { get; set; }
        public new string? Name { get; set; }
		public new string? Description { get; set; }
		public new string? PhoneNumber { get; set; }
		public new string? Location { get; set; }
		public new DateTime? StartsOn { get; set; }
		public new DateTime? EndsOn { get; set; }
		public new TimeSpan? StartsAt { get; set; }
		public new TimeSpan? EndsAt { get; set; }
		public new Frequency? Frequency { get; set; }
		public new uint? Separation { get; set; }
		//public new TimeSpan? CreatedOn { get; set; }
		public new int? RecurrenceDay { get; set; }
		public new int? RecurrenceWeek { get; set; }
		public new int? RecurrenceMonth { get; set; }
	}

}
