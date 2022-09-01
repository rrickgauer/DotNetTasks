using System.Data;
using Tasks.Domain.Parms;
using Tasks.Validation;

namespace Tasks.Repositories.Interfaces
{
    public interface IRecurrenceRepository
    {
        public Task<DataTable> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval);
        public Task<DataTable> GetEventRecurrencesAsync(EventRecurrenceRetrieval eventRecurrenceRetrieval);
        public Task<DataTable> GetRecurrencesForRemindersAsync(IValidDateRange validDateRange);
    }
}
