using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Security;
using Tasks.Services.Interfaces;

namespace Tasks.Controllers
{
    /// <summary>
    /// Url Prefix: /cancellations
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("cancellations")]
    public class CancellationsController : ControllerBase
    {
        #region Private members
        private readonly IConfigs _configuration;
        private readonly IEventActionServices _eventCompletionServices;
        private readonly IEventServices _eventServices;
        private Guid CurrentUserId => SecurityMethods.GetUserIdFromRequest(Request).Value;
        #endregion

        /// <summary>
        /// Constructor with DI
        /// </summary>
        /// <param name="configs"></param>
        /// <param name="eventActionServices"></param>
        /// <param name="eventServices"></param>
        public CancellationsController(IConfigs configs, IEventActionServices eventActionServices, IEventServices eventServices)
        {
            _configuration = configs;
            _eventCompletionServices = eventActionServices;
            _eventServices = eventServices;
        }


        /// <summary>
        /// PUT: /cancellations/:eventId/:onDate
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="onDate"></param>
        /// <returns></returns>
        [HttpPut("{eventId}/{onDate}")]
        public async Task<ActionResult<EventAction>> CreateCancellationAsync([FromRoute] Guid eventId, [FromRoute] DateTime onDate)
        {
            // make sure user owns the event before marking it complete
            var clientOwnsEvent = await _eventServices.ClientOwnsEventAsync(eventId, CurrentUserId);

            if (!clientOwnsEvent)
            {
                return NotFound();
            }

            // save it to the database
            var newCompletion = await _eventCompletionServices.SaveEventCancellationAsync(eventId, onDate);

            return Ok(newCompletion);
        }


    }
}
