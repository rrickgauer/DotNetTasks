using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Validation;

namespace Tasks.Services.Interfaces
{
    public interface IRecurrenceServices
    {
        public Task<List<Recurrence>> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval);
        public Task<List<Recurrence>> GetEventRecurrencesAsync(EventRecurrenceRetrieval eventRecurrenceRetrieval);
        public Task<IEnumerable<Recurrence>> GetRecurrencesForRemindersAsync(IValidDateRange validDateRange);
    }
}
