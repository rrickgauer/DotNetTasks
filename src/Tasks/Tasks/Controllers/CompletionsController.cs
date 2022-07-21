using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Configurations;
using Tasks.Security;
using Tasks.Services.Interfaces;

#pragma warning disable CS8629 // Nullable value type may be null.

namespace Tasks.Controllers
{
    [Authorize]
    [ApiController]
    [Route("completions")]
    public class CompletionsController : ControllerBase
    {
        #region Private members
        private readonly IConfigs _configuration;
        private readonly IEventActionServices _eventCompletionServices;
        private readonly IEventServices _eventServices;
        private Guid CurrentUserId => SecurityMethods.GetUserIdFromRequest(Request).Value;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public CompletionsController(IConfigs configuration, IEventActionServices eventCompletionServices, IEventServices eventServices)
        {
            _configuration = configuration;
            _eventCompletionServices = eventCompletionServices;
            _eventServices = eventServices; 
        }

        /// <summary>
        /// GET: /completions/:eventId/:onDate
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpPut("{eventId}/{onDate}")]
        public ActionResult<string> CreateCompletion([FromRoute]Guid eventId, [FromRoute]DateTime onDate)
        {
            // make sure user owns the event before marking it complete
            if (!_eventServices.ClientOwnsEvent(eventId))
            {
                return NotFound();
            }

            var newCompletion = _eventCompletionServices.SaveEventCompletion(CurrentUserId, eventId, onDate);

            return Ok(newCompletion);
        }

        /// <summary>
        /// DELETE: /completions/:eventId/:onDate
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpDelete("{eventId}/{onDate}")]
        public ActionResult<string> DeleteCompletion([FromRoute] Guid eventId, [FromRoute] DateTime onDate)
        {
            // make sure user owns the event before marking it complete
            if (!_eventServices.ClientOwnsEvent(eventId))
            {
                return NotFound();
            }

            var recordDeleted = _eventCompletionServices.DeleteEventCompletion(eventId, onDate);

            if (!recordDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
