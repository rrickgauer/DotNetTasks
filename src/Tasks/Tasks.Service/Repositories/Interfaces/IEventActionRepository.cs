using System.Data;
using Tasks.Service.Domain.Models;

namespace Tasks.Service.Repositories.Interfaces;

public interface IEventActionRepository
{
    public Task<int> ModifyEventActionAsync(EventAction eventAction);
    public Task<int> DeleteEventActionAsync(EventAction eventAction);
    public Task<DataRow?> GetEventActionAsync(EventAction eventAction);
}
