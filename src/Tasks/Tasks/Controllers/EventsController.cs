using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tasks.Domain.Models;

namespace Tasks.Controllers
{
    [ApiController]
    [Route("events")]
    public class EventsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<EventDto>> GetEvents()
        {
            EventDto dto = new();

            List<object> events = new() { dto };

            return Ok(events);
        }
    }
}
