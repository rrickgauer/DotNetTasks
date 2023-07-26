using System.Data;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Validation;

namespace Tasks.Service.Repositories.Interfaces;

public interface IRecurrenceRepository
{
    public Task<DataTable> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval);
    public Task<DataTable> GetRecurrencesAsync(RecurrenceRetrieval eventRecurrenceRetrieval, Guid eventId);
    public Task<DataTable> GetRecurrencesForRemindersAsync(IValidDateRange validDateRange);
}
