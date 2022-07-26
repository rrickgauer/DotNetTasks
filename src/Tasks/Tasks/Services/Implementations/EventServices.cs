using System.Data;
using Tasks.Domain.Models;
using Tasks.Mappers;
using Tasks.Repositories.Interfaces;
using Tasks.Security;
using Tasks.Services.Interfaces;
using System.Linq;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8629 // Nullable value type may be null.

namespace Tasks.Services.Implementations
{
    public class EventServices : IEventServices
    {
        #region Private members
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEventRepository _eventRepository;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="eventRepository"></param>
        public EventServices(IHttpContextAccessor httpContextAccessor, IEventRepository eventRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _eventRepository = eventRepository;
        }








        /// <summary>
        /// Delete an event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool DeleteEvent(Guid eventId)
        {
            int numRowsAfftected = _eventRepository.DeleteEvent(eventId);

            return numRowsAfftected > 0;
        }



        public async Task<bool> DeleteEventAsync(Guid eventId)
        {
            int numRowsAfftected = await _eventRepository.DeleteEventAsync(eventId);

            return numRowsAfftected > 0;
        }






















        /// <summary>
        /// Get the event that is owned by the current client id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Event? GetUserEvent(Guid eventId)
        {
            DataRow? dr = _eventRepository.GetEvent(eventId);

            if (dr == null)
            {
                return null;
            }

            Event? theEvent = EventMapper.ToModel(dr);

            if (theEvent == null ||  theEvent.UserId != GetCurrentUserId())
            {
                return null;
            }

            return theEvent;
        }

        /// <summary>
        /// Get the event that is owned by the current client id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public async Task<Event?> GetUserEventAsync(Guid eventId)
        {
            DataRow? dr = await _eventRepository.GetEventAsync(eventId);

            if (dr == null)
            {
                return null;
            }

            Event theEvent = EventMapper.ToModel(dr);

            if (theEvent.UserId != GetCurrentUserId())
            {
                return null;
            }

            return theEvent;
        }

        /// <summary>
        /// Get the specified event.
        /// Returns null if the event id does not exist.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Event? GetEvent(Guid eventId)
        {
            DataRow? dr = _eventRepository.GetEvent(eventId);

            Event? theEvent = dr != null ? EventMapper.ToModel(dr) : null;

            return theEvent;
        }


        public async Task<Event?> GetEventAsync(Guid eventId)
        {
            DataRow? dr = await _eventRepository.GetEventAsync(eventId);

            Event? theEvent = dr != null ? EventMapper.ToModel(dr) : null;

            return theEvent;
        }








        /// <summary>
        /// Get a list of events owned by the current user
        /// </summary>
        /// <returns></returns>
        public List<Event> GetUserEvents()
        {
            var clientUserId = GetCurrentUserId().Value;
            var table = _eventRepository.GetUserEvents(clientUserId);
            var eventDataRows = table.AsEnumerable();

            var events =
                from dataRow
                in eventDataRows
                select EventMapper.ToModel(dataRow);

            return events.ToList();
        }


        public async Task<List<Event>> GetUserEventsAsync()
        {
            var clientUserId = GetCurrentUserId();
            var table = await _eventRepository.GetUserEventsAsync(clientUserId.Value);
            var events = from dataRow in table.AsEnumerable() select EventMapper.ToModel(dataRow);

            return events.ToList();
        }









        /// <summary>
        /// Modify the specified event
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        //public bool ModifyEvent(Event e)
        //{
        //    int numRowsAffected = _eventRepository.ModifyEvent(e);
        //    return numRowsAffected >= 0;
        //}


        /// <summary>
        /// Modify the specified event
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task<bool> ModifyEventAsync(Event e)
        {
            int numRowsAffected = await _eventRepository.ModifyEventAsync(e);

            return numRowsAffected >= 0;
        }





        /// <summary>
        /// Get the current user id
        /// </summary>
        /// <returns></returns>
        private Guid? GetCurrentUserId()
        {
            Guid? userId = null;
            
            var context = _httpContextAccessor.HttpContext;

            if (context != null)
            {
                userId = SecurityMethods.GetUserIdFromRequest(context.Request);
            }

            return userId;
        }






        /// <summary>
        /// Create a new event
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public Event CreateNewEvent(Event eventData)
        {
            Event newEvent = eventData;

            // create a new event object
            newEvent.Id = Guid.NewGuid();
            newEvent.UserId = GetCurrentUserId();

            // save it in the database
            //ModifyEvent(newEvent);

            return newEvent;
        }


        public async Task<Event> CreateNewEventAsync(Event eventData)
        {
            Event newEvent = eventData;

            // create a new event object
            newEvent.Id = Guid.NewGuid();
            newEvent.UserId = GetCurrentUserId();

            // save it in the database
            await ModifyEventAsync(newEvent);

            return newEvent;
        }












        /// <summary>
        /// Check if the current user owns the event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool ClientOwnsEvent(Guid eventId)
        {
            var e = GetUserEvent(eventId);

            if (e == null)
            {
                return false;
            }

            return true;
        }



        public async Task<bool> ClientOwnsEventAsync(Guid eventId)
        {
            var e = await GetUserEventAsync(eventId);

            if (e == null)
            {
                return false;
            }

            return true;
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

        public async Task<bool> ClientOwnsEventAsync(Event? e)
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
















        /// <summary>
        /// Update the event
        /// </summary>
        /// <param name="eventBody"></param>
        /// <returns></returns>
        public Event UpdateEvent(Event eventBody)
        {
            // create a new event object
            eventBody.UserId = GetCurrentUserId();

            // save it in the database
            //ModifyEvent(eventBody);

            return eventBody;
        }


        public async Task<Event> UpdateEventAsync(Event eventBody)
        {
            // create a new event object
            eventBody.UserId = GetCurrentUserId();

            // save it in the database
            await ModifyEventAsync(eventBody);

            return eventBody;
        }


    }
}
