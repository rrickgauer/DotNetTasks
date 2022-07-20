using System.Data;
using Tasks.Domain.Parms;

namespace Tasks.Repositories.Interfaces
{
    public interface IRecurrenceRepository
    {
        public DataTable GetRecurrences(RecurrenceRetrieval recurrenceRetrieval);
        public DataTable GetEventRecurrences(EventRecurrenceRetrieval eventRecurrenceRetrieval);
    }
}
