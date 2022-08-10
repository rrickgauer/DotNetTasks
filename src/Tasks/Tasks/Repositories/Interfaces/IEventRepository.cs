using System.Data;
using Tasks.Domain.Models;
namespace Tasks.Repositories.Interfaces
{
    public interface IEventRepository
    {
        public Task<DataTable> GetUserEventsAsync(Guid userId);
        public Task<DataRow?> GetEventAsync(Guid eventId);
        public Task<int> DeleteEventAsync(Guid eventId);
        public Task<int> ModifyEventAsync(Event e);
    }
}
