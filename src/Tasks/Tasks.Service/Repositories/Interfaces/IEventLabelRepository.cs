using System.Data;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;

namespace Tasks.Service.Repositories.Interfaces;

public interface IEventLabelRepository
{
    public Task<int> InsertAsync(EventLabel eventLabel);
    public Task<DataTable> SelectAllForEventAsync(Guid eventId);
    public Task<int> InsertBatchAsync(EventLabelsBatchRequest eventLabelsBatchRequest);
    public Task<DataTable> SelectAllAsync(Guid userId);
    public Task<int> DeleteAsync(Guid eventId, Guid labelId);

}
