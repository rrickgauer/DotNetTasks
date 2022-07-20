using Tasks.Domain.Models;
using Tasks.Domain.Parms;

namespace Tasks.Services.Interfaces
{
    public interface IRecurrenceServices
    {
        public List<Recurrence> GetRecurrences(RecurrenceRetrieval recurrenceRetrieval);
        public List<Recurrence> GetEventRecurrences(EventRecurrenceRetrieval eventRecurrenceRetrieval);
    }
}
