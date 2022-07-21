using Tasks.Domain.Models;
using Tasks.Domain.Parms;

namespace Tasks.Services.Interfaces
{
    public interface IEventActionServices
    {
        public EventAction SaveEventCompletion(Guid eventId, DateTime onDate);
        public bool DeleteEventCompletion(Guid eventId, DateTime onDate);
        public EventAction? GetEventCompletion(Guid eventId, DateTime onDate);
        
        //public void SaveEventCancellation(Guid eventId, DateTime onDate);
        //public void DeleteEventCancellation(Guid eventId, DateTime onDate);
    }
}
