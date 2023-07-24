using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Domain.Views;
using Tasks.Service.Validation;

namespace Tasks.Service.Services.Interfaces;

public interface IRecurrenceServices
{
    public Task<IEnumerable<Recurrence>> GetRecurrencesForRemindersAsync(IValidDateRange validDateRange);
    public Task<IEnumerable<GetRecurrencesResponse>> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval);
    public Task<IEnumerable<GetRecurrencesResponse>> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval, Guid eventId);
}
