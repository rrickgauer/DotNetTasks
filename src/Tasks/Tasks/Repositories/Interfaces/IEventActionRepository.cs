using Tasks.Domain.Models;

namespace Tasks.Repositories.Interfaces
{
    public interface IEventActionRepository
    {
        public int ModifyEventAction(EventAction eventAction);
        public int DeleteEventAction(EventAction eventAction);
    }
}
