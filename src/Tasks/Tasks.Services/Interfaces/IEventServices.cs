using Tasks.Domain.Models;

namespace Tasks.Services.Interfaces
{
    public interface IEventServices
    {
        public Task<List<Event>> GetUserEventsAsync();
        public Task<Event?> GetUserEventAsync(Guid eventId);
        public Task<Event?> GetEventAsync(Guid eventId);
        public Task<bool> DeleteEventAsync(Guid eventId);
        public Task<Event> UpdateEventAsync(Event eventData);
        public Task<Event> CreateNewEventAsync(Event eventData);
        public Task<bool> ClientOwnsEventAsync(Event? e);
        public Task<bool> ClientOwnsEventAsync(Guid eventId);
    }
}
