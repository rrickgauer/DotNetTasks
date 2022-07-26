using Tasks.Domain.Models;
using Tasks.Domain.Parms;

namespace Tasks.Services.Interfaces
{
    public interface IEventActionServices
    {
        //public EventAction SaveEventCompletion(Guid eventId, DateTime onDate);
        //public bool DeleteEventCompletion(Guid eventId, DateTime onDate);
        //public EventAction? GetEventCompletion(Guid eventId, DateTime onDate);



        public Task<EventAction> SaveEventCompletionAsync(Guid eventId, DateTime onDate);
        public Task<bool> DeleteEventCompletionAsync(Guid eventId, DateTime onDate);
        public Task<EventAction?> GetEventCompletionAsync(Guid eventId, DateTime onDate);
    }
}
