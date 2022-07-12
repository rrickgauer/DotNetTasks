using Tasks.Domain.Models;
namespace Tasks.Repositories.Interfaces
{
    public interface IEventRepository
    {
        public List<Event> GetEvents();
    }
}
