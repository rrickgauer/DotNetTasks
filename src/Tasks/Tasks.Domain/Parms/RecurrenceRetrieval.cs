using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tasks.Validation;

namespace Tasks.Domain.Parms
{
    public class RecurrenceRetrieval : IValidDateRange
    {
        public Guid UserId { get; set; }

        [BindRequired] 
        public DateTime StartsOn { get; set; }

        [BindRequired] 
        public DateTime EndsOn { get; set; }

        public bool IsValid() => EndsOn >= StartsOn;
    }
}
