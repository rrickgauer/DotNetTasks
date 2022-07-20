using Tasks.Domain.Models;
using Tasks.Domain.Parms;

namespace Tasks.Services.Interfaces
{
    public interface IEventActionServices
    {
        public EventAction SaveEventCompletion(Guid userId, Guid eventId, DateTime onDate);
        //public EventAction SaveEventCompletion(EventActionParms eventActionParms);
        public void DeleteEventCompletion(Guid eventId, DateTime onDate);
        
        //public void SaveEventCancellation(Guid eventId, DateTime onDate);
        //public void DeleteEventCancellation(Guid eventId, DateTime onDate);
    }
}
