using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Services.Interfaces;

namespace Tasks.Controllers
{
    [Authorize]
    [ApiController]
    [Route("recurrences")]
    public class RecurrencesController : ControllerBase
    {
        #region Private members
        private readonly IConfigs _configuration;
        private readonly IRecurrenceServices _recurrenceServices;
        #endregion

        /// <summary>
        /// Constructor.
        /// Dependencies are injected into the contructor.
        /// </summary>
        /// <param name="configs"></param>
        /// <param name="recurrenceServices"></param>
        public RecurrencesController(IConfigs configs, IRecurrenceServices recurrenceServices)
        {
            _configuration = configs;
            _recurrenceServices = recurrenceServices;
        }


        /// <summary>
        /// GET: /recurrences
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Recurrence>> GetRecurrences()
        {
            return Ok("get all recurrences");
        }

        /// <summary>
        /// GET: /recurrences/:eventId
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet("{eventId}")]
        public ActionResult<List<Recurrence>> GetEventRecurrences(Guid eventId)
        {
            return Ok("get event recurrences");
        }


    }
}
