using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Repositories.Implementations;
using Tasks.Repositories.Interfaces;
using Tasks.Security;
using Tasks.Services.Interfaces;

namespace Tasks.Controllers
{
    [Authorize]
    [ApiController]
    [Route("events")]
    public class EventsController : ControllerBase
    {
        private readonly IConfigs _configuration;
        private readonly IEventServices _eventServices;
        private Guid CurrentUserId => SecurityMethods.GetUserIdFromRequest(Request).Value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="eventRepository"></param>
        public EventsController(IConfigs configuration, IEventServices eventServices)
        {
            _configuration = configuration;
            _eventServices = eventServices;
        }

        /// <summary>
        /// GET: /events
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Event>> GetEvents()
        {
            var userEvents = _eventServices.GetUserEvents();
            return Ok(userEvents);
        }

        /// <summary>
        /// GET: /events/:eventId
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet("{eventId}")]
        public ActionResult<Event> GetEvent(Guid eventId)
        {
            var e = _eventServices.GetUserEvent(eventId);
            if (e == null)
            {
                return NotFound();
            }

            return Ok(e);
        }

        /// <summary>
        /// DELETE: /events/:eventId
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpDelete("{eventId}")]
        public IActionResult DeleteEvent(Guid eventId)
        {
            var userEvent = _eventServices.GetUserEvent(eventId);

            if (userEvent == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// PUT: /events/eventId
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="eventBody"></param>
        /// <returns></returns>
        [HttpPut("{eventId}")]
        public ActionResult<Event> UpdateEvent(Guid eventId, [FromForm] Event eventBody)
        {
            Event? existingEvent = _eventServices.GetEvent(eventId);

            // check if an event with this id already exists or is owned by another user
            if (existingEvent != null && existingEvent.UserId != CurrentUserId)
            {
                return Forbid();
            }

            // set the event to the id in the url and save it
            eventBody.Id = eventId;
            _eventServices.UpdateEvent(eventBody);

            var updatedEvent = _eventServices.GetUserEvent(eventId);
            if (existingEvent == null)
            {
                return Created($"{Request.Path}", updatedEvent);    // created a new event
            }
            else
            {
                return Ok(updatedEvent);                            // updated an existing event
            }
        }

        /// <summary>
        /// POST: /events
        /// </summary>
        /// <param name="eventFromBody"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Event> CreateEvent([FromForm] Event eventFromBody)
        {
            Event newEvent = _eventServices.CreateNewEvent(eventFromBody);

            // return it
            return Created($"{Request.Path}/{newEvent.Id}", newEvent);    // created a new event
        }



    }
}
