using System.Data;
using Tasks.Domain.Models;
using Tasks.Mappers;
using Tasks.Repositories.Interfaces;
using Tasks.Security;
using Tasks.Services.Interfaces;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8629 // Nullable value type may be null.

namespace Tasks.Services.Implementations
{
    public class EventServices : IEventServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEventRepository _eventRepository;

        public EventServices(IHttpContextAccessor httpContextAccessor, IEventRepository eventRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _eventRepository = eventRepository;
        }

        public bool DeleteEvent(Guid eventId)
        {
            int numRowsAfftected = _eventRepository.DeleteEvent(eventId);

            return numRowsAfftected > 0;
        }

        public Event? GetEventUser(Guid eventId)
        {
            DataRow? dr = _eventRepository.GetEvent(eventId);

            Event? theEvent = dr != null ? EventMapper.ToModel(dr) : null;

            if (theEvent == null)
            {
                return null;
            }
            else if (theEvent.UserId != GetCurrentUserId())
            {
                return null;
            }

            return theEvent;
        }

        public Event? GetEvent(Guid eventId)
        {
            DataRow? dr = _eventRepository.GetEvent(eventId);

            Event? theEvent = dr != null ? EventMapper.ToModel(dr) : null;

            return theEvent;
        }

        public List<Event> GetEventsUser()
        {
            var clientUserId = GetCurrentUserId().Value;
            List<Event> events = new();

            foreach (DataRow dr in _eventRepository.GetUserEvents(clientUserId).Rows)
            {
                Event e = EventMapper.ToModel(dr);
                events.Add(e);
            }

            return events;
        }


        public bool ModifyEvent(Event e)
        {
            int numRowsAffected = _eventRepository.ModifyEvent(e);
            return numRowsAffected >= 0;
        }


        /// <summary>
        /// Get the current user id
        /// </summary>
        /// <returns></returns>
        private Guid? GetCurrentUserId()
        {
            return SecurityMethods.GetUserIdFromRequest(_httpContextAccessor.HttpContext.Request);
        }

        public Event CreateNewEvent(Event eventData)
        {
            Event newEvent = eventData;

            // create a new event object
            newEvent.Id = Guid.NewGuid();
            newEvent.UserId = GetCurrentUserId();

            // save it in the database
            ModifyEvent(newEvent);

            return newEvent;
        }

        /// <summary>
        /// check if an event with this id already exists
        /// if so, make sure the client owns that event already
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool ClientOwnsEvent(Event? e)
        {
            if (e == null)
            {
                return false;
            }
            else if (e.UserId.Value != GetCurrentUserId())
            {
                return false;
            }

            return true;
        }


        public Event UpdateEvent(Event eventBody)
        {
            // create a new event object
            eventBody.UserId = GetCurrentUserId();

            // save it in the database
            ModifyEvent(eventBody);

            return eventBody;
        }
    }
}
