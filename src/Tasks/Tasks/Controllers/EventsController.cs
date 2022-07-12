using Microsoft.AspNetCore.Mvc;

namespace Tasks.Controllers
{
    [ApiController]
    [Route("events")]
    public class EventsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetEvents()
        {
            return Ok("Get all events");
        }
    }
}
