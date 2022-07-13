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
        //[AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Event>> GetEvents()
        {
            return Ok(_eventRepository.GetEvents());
        }
    }
}
