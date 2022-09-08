using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Domain.Views;
using Tasks.Validation;

namespace Tasks.Services.Interfaces
{
    public interface IRecurrenceServices
    {
        //public Task<List<Recurrence>> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval);
        //public Task<List<Recurrence>> GetEventRecurrencesAsync(EventRecurrenceRetrieval eventRecurrenceRetrieval);

        public Task<IEnumerable<Recurrence>> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval);
        public Task<IEnumerable<Recurrence>> GetEventRecurrencesAsync(EventRecurrenceRetrieval eventRecurrenceRetrieval);

        public Task<IEnumerable<Recurrence>> GetRecurrencesForRemindersAsync(IValidDateRange validDateRange);



        public Task<IEnumerable<GetRecurrencesResponse>> GetRecurrencesAsync_NEW(RecurrenceRetrieval recurrenceRetrieval);





    }
}
