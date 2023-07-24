using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;

namespace Tasks.Service.Repositories.Interfaces;

public interface IEventLabelRepository
{
    public Task<int> InsertAsync(EventLabel eventLabel, Guid userId);
    public Task<DataTable> SelectAllAsync(Guid eventId, Guid userId);
    public Task<int> InsertBatchAsync(EventLabelsBatchRequest eventLabelsBatchRequest);
    public Task<DataTable> SelectAllAsync(Guid userId);
    public Task<int> DeleteAsync(Guid eventId, Guid labelId);

}
