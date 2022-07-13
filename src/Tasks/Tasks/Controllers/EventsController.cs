using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Repositories.Implementations;
using Tasks.Repositories.Interfaces;

namespace Tasks.Controllers
{
    [ApiController]
    [Route("events")]
    public class EventsController : ControllerBase
    {
        private readonly IConfigs _configuration;
        private readonly IEventRepository _eventRepository;

        public EventsController(IConfigs configuration)
        {
            _configuration = configuration;
            _eventRepository = new EventRepository(configuration);
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
    }
}
