using Tasks.Domain.Models;
using Tasks.Domain.Parms;

namespace Tasks.Services.Interfaces
{
    public interface IEventActionServices
    {
        public Task<EventAction> SaveEventCompletionAsync(Guid eventId, DateTime onDate);
        public Task<bool> DeleteEventCompletionAsync(Guid eventId, DateTime onDate);
        public Task<EventAction?> GetEventCompletionAsync(Guid eventId, DateTime onDate);


        public Task<EventAction> SaveEventCancellationAsync(Guid eventId, DateTime onDate);
    }
}
