using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;

namespace Tasks.Repositories.Interfaces;

public interface IEventLabelRepository
{
    public Task<int> InsertAsync(EventLabel eventLabel, Guid userId);
    public Task<DataTable> SelectAllAsync(Guid eventId, Guid userId);
    public Task<int> InsertBatchAsync(EventLabelsBatchRequest eventLabelsBatchRequest);

    public Task<DataTable> SelectAllAsync(Guid userId);

}
