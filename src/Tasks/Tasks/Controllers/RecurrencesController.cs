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
using Tasks.Validation;

#pragma warning disable CS8629 // Nullable value type may be null.

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
        private Guid CurrentUserId => SecurityMethods.GetUserIdFromRequest(Request).Value;
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
        public async Task<ActionResult<List<Recurrence>>> GetRecurrencesAsync([FromQuery] RecurrenceRetrieval retrieval)
        {
            try
            {
                ValidateRetrievalRange(retrieval);
            }
            catch (ValidationException err)
            {
                return BadRequest(err.Message);
            }

            // fill out the remaining RecurrenceRetrieval property values
            retrieval.UserId = SecurityMethods.GetUserIdFromRequest(Request).Value;

            // get the recurrences
            var recurrences = await _recurrenceServices.GetRecurrencesAsync(retrieval);

            return Ok(recurrences);
        }

        /// <summary>
        /// GET: /recurrences/:eventId
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpGet("{eventId}")]
        public async Task<ActionResult<List<Recurrence>>> GetEventRecurrencesAsync(Guid eventId, [FromQuery] EventRecurrenceRetrieval retrieval)
        {
            try
            {
                ValidateRetrievalRange(retrieval);
            }
            catch (ValidationException err)
            {
                return BadRequest(err.Message);
            }

            // make sure the user owns the requested event
            var userEvent = await _eventServices.GetEventAsync(eventId, CurrentUserId);

            if (userEvent == null)
            {
                return NotFound();
            }

            // fill out the remaining EventRecurrenceRetrieval property values
            retrieval.UserId = SecurityMethods.GetUserIdFromRequest(Request).Value;
            retrieval.EventId = eventId;

            // get the recurrences
            var recurrences = await _recurrenceServices.GetEventRecurrencesAsync(retrieval);

            return Ok(recurrences);
        }

        /// <summary>
        /// Validate that the given range is valid
        /// </summary>
        /// <param name="range"></param>
        /// <exception cref="ValidationException"></exception>
        private void ValidateRetrievalRange(IValidDateRange range)
        {
            if (!range.IsValid())
            {
                throw new ValidationException("EndsOn must be greater than or equal to StartsOn");
            }
        }




        /// <summary>
        /// GET: /recurrences
        /// </summary>
        /// <returns></returns>
        [HttpGet("test-all")]
        public async Task<IActionResult> GetRecurrencesAsyncTestNew([FromQuery] RecurrenceRetrieval retrieval)
        {
            try
            {
                ValidateRetrievalRange(retrieval);
            }
            catch (ValidationException err)
            {
                return BadRequest(err.Message);
            }

            retrieval.UserId = CurrentUserId;

            var result = await _recurrenceServices.GetRecurrencesAsync_NEW(retrieval);

            return Ok(result);

        }

    }
}
