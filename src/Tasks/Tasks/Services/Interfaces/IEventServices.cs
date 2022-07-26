﻿using Tasks.Domain.Models;

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
        public Task<List<Event>> GetUserEventsAsync();


        public Event? GetEvent(Guid eventId);
        public Event? GetUserEvent(Guid eventId);
        public bool DeleteEvent(Guid eventId);
        public Event UpdateEvent(Event eventData);
        public Event CreateNewEvent(Event eventData);
        public bool ClientOwnsEvent(Event? e);
        public bool ClientOwnsEvent(Guid eventId);
    }
}
