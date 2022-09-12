using System.Data;
using Tasks.Domain.Parms;
using Tasks.Validation;

namespace Tasks.Repositories.Interfaces
{
    public interface IRecurrenceRepository
    {
        public Task<DataTable> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval);
        public Task<DataTable> GetRecurrencesAsync(RecurrenceRetrieval eventRecurrenceRetrieval, Guid eventId);
        public Task<DataTable> GetRecurrencesForRemindersAsync(IValidDateRange validDateRange);
    }
}
