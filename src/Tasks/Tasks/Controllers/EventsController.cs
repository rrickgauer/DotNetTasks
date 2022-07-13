using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
//using System.Web.Http;
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

        public EventsController(IConfigs configuration)
        {
            _configuration = configuration;
            _eventRepository = new EventRepository(configuration);
        }

        /// <summary>
        /// Get all events for the user
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Event>> GetEvents()
        {
            return Ok(_eventRepository.GetEvents());
        }
    }
}
