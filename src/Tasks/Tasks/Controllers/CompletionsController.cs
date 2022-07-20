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
        #endregion

        private Guid CurrentUserId => SecurityMethods.GetUserIdFromRequest(Request).Value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public CompletionsController(IConfigs configuration, IEventActionServices eventCompletionServices)
        {
            _configuration = configuration;
            _eventCompletionServices = eventCompletionServices;
        }

        /// <summary>
        /// GET: /events/:eventId
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpPut("{eventId}/{onDate}")]
        public ActionResult<string> CreateCompletion([FromRoute]Guid eventId, [FromRoute]DateTime onDate)
        {
            var newCompletion = _eventCompletionServices.SaveEventCompletion(CurrentUserId, eventId, onDate);
            return Ok(newCompletion);
        }
    }
}
