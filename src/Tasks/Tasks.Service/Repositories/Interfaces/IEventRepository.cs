using System.Data;
using Tasks.Service.Domain.Models;
namespace Tasks.Service.Repositories.Interfaces
{
    public interface IEventRepository
    {
        public Task<DataTable> GetUserEventsAsync(Guid userId);
        public Task<DataRow?> GetEventAsync(Guid eventId);
        public Task<int> DeleteEventAsync(Guid eventId);
        public Task<int> ModifyEventAsync(Event e);
    }
}
