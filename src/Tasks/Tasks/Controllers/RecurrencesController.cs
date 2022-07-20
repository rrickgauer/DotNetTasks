using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Security;
using Tasks.Services.Interfaces;

namespace Tasks.Controllers
{
    /// <summary>
    /// URL prefix: /recurrences
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("recurrences")]
    public class RecurrencesController : ControllerBase
    {
        #region Private members
        private readonly IConfigs _configuration;
        private readonly IRecurrenceServices _recurrenceServices;
        private readonly IEventServices _eventServices;
        #endregion

        /// <summary>
        /// Constructor
        /// Dependencies are injected into the contructor.
        /// </summary>
        /// <param name="configs"></param>
        /// <param name="recurrenceServices"></param>
        public RecurrencesController(IConfigs configs, IRecurrenceServices recurrenceServices, IEventServices eventServices)
        {
            _configuration = configs;
            _recurrenceServices = recurrenceServices;
            _eventServices = eventServices; 
        }

        /// <summary>
        /// GET: /recurrences
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Recurrence>> GetRecurrences([FromQuery] RecurrenceRetrieval retrieval)
        {
            // fill out the remaining RecurrenceRetrieval property values
            retrieval.UserId = SecurityMethods.GetUserIdFromRequest(Request).Value;

            // get the recurrences
            var recurrences = _recurrenceServices.GetRecurrences(retrieval);

            return Ok(recurrences);
        }

        /// <summary>
        /// GET: /recurrences/:eventId
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet("{eventId}")]
        public ActionResult<List<Recurrence>> GetEventRecurrences(Guid eventId, [FromQuery] EventRecurrenceRetrieval retrieval)
        {
            // make sure the user owns the requested event
            var userEvent = _eventServices.GetUserEvent(eventId);
            if (userEvent == null)
            {
                return NotFound();
            }

            // fill out the remaining EventRecurrenceRetrieval property values
            retrieval.UserId = SecurityMethods.GetUserIdFromRequest(Request).Value;
            retrieval.EventId = eventId;

            // get the recurrences
            var recurrences = _recurrenceServices.GetEventRecurrences(retrieval);

            return Ok(recurrences);
        }


    }
}
