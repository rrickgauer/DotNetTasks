using Tasks.Domain.Models;
namespace Tasks.Repositories.Interfaces
{
    public interface IEventRepository
    {
        public List<Event> GetUserEvents();
        public Event? GetUserEvent(Guid eventId);
        public bool DeleteEvent(Guid eventId);
        public bool ModifyEvent(Event e);
        //public bool EventEx
        public Event? GetEvent(Guid eventId);
    }
}
