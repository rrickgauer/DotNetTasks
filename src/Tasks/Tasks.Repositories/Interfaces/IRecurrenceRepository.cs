using System.Data;
using Tasks.Domain.Parms;

namespace Tasks.Repositories.Interfaces
{
    public interface IRecurrenceRepository
    {
        public Task<DataTable> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval);
        public Task<DataTable> GetEventRecurrencesAsync(EventRecurrenceRetrieval eventRecurrenceRetrieval);
    }
}
