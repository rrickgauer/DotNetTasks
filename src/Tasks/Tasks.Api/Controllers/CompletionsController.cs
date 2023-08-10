using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Service.Domain.Models;
using Tasks.Service.Configurations;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Security;
using Tasks.Service.Auth;
using Tasks.Api.Controllers.Bases;

#pragma warning disable CS8629 // Nullable value type may be null.

namespace Tasks.Api.Controllers;

/// <summary>
/// URL Prefix: /completions
/// </summary>
[Authorize]
[ApiController]
[Route("completions")]
[ServiceFilter(typeof(EventAuthFilter))]                // ensure client is authorized to access the event
public class CompletionsController : AuthorizedControllerBase
{
    #region Private members
    private readonly IConfigs _configuration;
    private readonly IEventActionServices _eventCompletionServices;
    #endregion

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
    /// PUT: /completions/:eventId/:onDate
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    [HttpPut("{eventId}/{onDate}")]
    public async Task<ActionResult<EventAction>> CreateCompletionAsync([FromRoute] Guid eventId, [FromRoute] DateTime onDate)
    {
        var newCompletion = await _eventCompletionServices.SaveEventCompletionAsync(eventId, onDate);

        return Ok(newCompletion);
    }

    /// <summary>
    /// Delete: /completions/:eventId/:onDate
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    [HttpDelete("{eventId}/{onDate}")]
    public async Task<IActionResult> DeleteCompletionAsync([FromRoute] Guid eventId, [FromRoute] DateTime onDate)
    {
        var recordDeleted = await _eventCompletionServices.DeleteEventCompletionAsync(eventId, onDate);

        if (!recordDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// GET: /completions/:eventId/:onDate
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="onDate"></param>
    /// <returns></returns>
    [HttpGet("{eventId}/{onDate}")]
    public async Task<ActionResult<EventAction>> GetCompletionAsync([FromRoute] Guid eventId, [FromRoute] DateTime onDate)
    {
        var eventCompletion = await _eventCompletionServices.GetEventCompletionAsync(eventId, onDate);

        if (eventCompletion == null)
        {
            return NotFound();  // return not found if there is no completion recorded for this event on this day
        }

        return Ok(eventCompletion);
    }

}
