using Tasks.Domain.Models;
using Tasks.Domain.Parms;

namespace Tasks.Services.Interfaces
{
    public interface IRecurrenceServices
    {
        public Task<List<Recurrence>> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval);
        public Task<List<Recurrence>> GetEventRecurrencesAsync(EventRecurrenceRetrieval eventRecurrenceRetrieval);
    }
}
