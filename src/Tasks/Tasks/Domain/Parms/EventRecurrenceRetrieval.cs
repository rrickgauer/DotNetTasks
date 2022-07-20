using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Tasks.Domain.Parms
{
    public class EventRecurrenceRetrieval
    {
        public Guid UserId { get; set; }
        [BindRequired] public DateTime StartsOn { get; set; }
        [BindRequired] public DateTime EndsOn { get; set; }
        public Guid EventId { get; set; }
    }

}
