using System.Data;
using Tasks.Domain.Models;

namespace Tasks.Repositories.Interfaces
{
    public interface IEventActionRepository
    {
        public Task<int> ModifyEventActionAsync(EventAction eventAction);
        public Task<int> DeleteEventActionAsync(EventAction eventAction);
        public Task<DataRow?> GetEventActionAsync(EventAction eventAction);
    }
}
