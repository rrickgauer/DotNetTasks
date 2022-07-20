using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Domain.Parms
{
    public class RecurrenceRetrieval
    {
        public Guid UserId { get; set; }

        [BindRequired] 
        public DateTime StartsOn { get; set; }

        [BindRequired] 
        public DateTime EndsOn { get; set; }
    }

}
