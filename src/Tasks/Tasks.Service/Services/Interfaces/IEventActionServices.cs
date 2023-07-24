using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;

namespace Tasks.Service.Services.Interfaces;

public interface IEventActionServices
{
    public Task<EventAction> SaveEventCompletionAsync(Guid eventId, DateTime onDate);
    public Task<bool> DeleteEventCompletionAsync(Guid eventId, DateTime onDate);
    public Task<EventAction?> GetEventCompletionAsync(Guid eventId, DateTime onDate);
    public Task<EventAction> SaveEventCancellationAsync(Guid eventId, DateTime onDate);
}
