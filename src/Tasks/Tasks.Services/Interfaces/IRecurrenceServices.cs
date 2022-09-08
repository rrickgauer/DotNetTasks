using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Domain.Views;
using Tasks.Validation;

namespace Tasks.Services.Interfaces
{
    public interface IRecurrenceServices
    {
        public Task<IEnumerable<Recurrence>> GetRecurrencesForRemindersAsync(IValidDateRange validDateRange);
        public Task<IEnumerable<GetRecurrencesResponse>> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval);
        public Task<IEnumerable<GetRecurrencesResponse>> GetRecurrencesAsync(EventRecurrenceRetrieval recurrenceRetrieval);
    }
}
