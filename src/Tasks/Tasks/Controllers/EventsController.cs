using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Repositories.Implementations;
using Tasks.Repositories.Interfaces;
using Tasks.Security;

namespace Tasks.Controllers
{
    [Authorize]
    [ApiController]
    [Route("events")]
    public class EventsController : ControllerBase
    {
        private readonly IConfigs _configuration;
        private readonly IEventRepository _eventRepository;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="eventRepository"></param>
        public EventsController(IConfigs configuration, IEventRepository eventRepository)
        {
            _configuration = configuration;
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// Get all events for the user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Event>> GetEvents()
        {
            return Ok(_eventRepository.GetUserEvents());
        }

        [HttpGet("{eventId}")]
        public ActionResult<Event> GetEvent(Guid eventId)
        {
            var e = _eventRepository.GetUserEvent(eventId);

            if (e == null)
            {
                return NotFound();
            }

            return Ok(e);
        }

        /// <summary>
        /// Delete event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpDelete("{eventId}")]
        public IActionResult DeleteEvent(Guid eventId)
        {
            var e = _eventRepository.GetUserEvent(eventId);

            if (e == null)
            {
                return NotFound();
            }

            _eventRepository.DeleteEvent(eventId);

            return NoContent();
        }

        [HttpPut("{eventId}")]
        public ActionResult<Event> UpdateEvent(Guid eventId, [FromForm] Event eventBody)
        {
            Event? existingEvent = _eventRepository.GetEvent(eventId);
            var clientUserId = SecurityMethods.GetUserIdFromRequest(Request);

            // check if an event with this id already exists
            // if so, make sure the client owns that event already
            if (existingEvent != null && existingEvent.UserId != clientUserId)
            {
                return Forbid();
            }

            // create a new event object
            eventBody.Id = eventId;
            eventBody.UserId = clientUserId;

            // save it in the database
            _eventRepository.ModifyEvent(eventBody);

            var eventFullyLoaded = _eventRepository.GetUserEvent(eventId);
            
            // determine if the event was created or updated
            if (existingEvent == null)
            {
                return Created($"{Request.Path}", eventFullyLoaded);    // created a new event
            }
            else
            {   
                return Ok(eventFullyLoaded);                            // updated an existing event
            }
        }

    }
}
