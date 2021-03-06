using Tasks.Domain.Enums;

namespace Tasks.Domain.Parms
{
    public class EventActionParms
    {
        public Guid EventId { get; set; }
        public DateTime OnDate { get; set; } = DateTime.Now;
        public EventActionType EventActionType { get; set; } = EventActionType.COMPLETION;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Guid UserId { get; set; }
    }
}
