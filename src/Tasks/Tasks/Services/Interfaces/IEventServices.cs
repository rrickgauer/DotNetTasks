using Tasks.Domain.Models;

namespace Tasks.Services.Interfaces
{
    public interface IEventServices
    {
        #region Old stuff
        //public List<Event> GetUserEvents();
        //public Event? GetEvent(Guid eventId);
        //public Event? GetUserEvent(Guid eventId);
        //public bool DeleteEvent(Guid eventId);
        //public Event UpdateEvent(Event eventData);
        //public Event CreateNewEvent(Event eventData);
        //public bool ClientOwnsEvent(Event? e);
        //public bool ClientOwnsEvent(Guid eventId);
        #endregion


        public List<Event> GetUserEvents();
        public Event? GetUserEvent(Guid eventId);
        public Event? GetEvent(Guid eventId);
        public bool DeleteEvent(Guid eventId);
        public Event UpdateEvent(Event eventData);
        public Event CreateNewEvent(Event eventData);


        public Task<List<Event>> GetUserEventsAsync();
        public Task<Event?> GetUserEventAsync(Guid eventId);
        public Task<Event?> GetEventAsync(Guid eventId);
        public Task<bool> DeleteEventAsync(Guid eventId);
        public Task<Event> UpdateEventAsync(Event eventData);
        public Task<Event> CreateNewEventAsync(Event eventData);
        public Task<bool> ClientOwnsEventAsync(Event? e);
        public Task<bool> ClientOwnsEventAsync(Guid eventId);


        public bool ClientOwnsEvent(Event? e);
        public bool ClientOwnsEvent(Guid eventId);
    }
}
