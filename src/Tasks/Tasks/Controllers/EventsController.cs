using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Repositories.Implementations;
using Tasks.Repositories.Interfaces;

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
            return Ok(_eventRepository.GetEvents());
        }

        [HttpGet("{eventId}")]
        public ActionResult<Event> GetEvent(Guid eventId)
        {
            var e = _eventRepository.GetEvent(eventId);

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
            var e = _eventRepository.GetEvent(eventId);

            if (e == null)
            {
                return NotFound();
            }

            _eventRepository.DeleteEvent(eventId);

            return NoContent();
        }
    }
}
