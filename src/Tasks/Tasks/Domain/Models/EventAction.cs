using System.Text.Json.Serialization;
using Tasks.CustomAttributes;
using Tasks.Domain.Enums;

namespace Tasks.Domain.Models
{
    public class EventAction
    {
        [SqlColumn("event_id")]
        public Guid? EventId { get; set; }

        [SqlColumn("on_date")]
        public DateTime? OnDate { get; set; }

        [JsonIgnore]
        [SqlColumn("event_action_type_id")]
        public EventActionType? EventActionType { get; set; }

        [SqlColumn("created_on")]
        public DateTime? CreatedOn { get; set; }

    }
}
