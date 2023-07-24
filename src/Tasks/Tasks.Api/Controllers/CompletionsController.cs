using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Service.Domain.Models;
using Tasks.Service.Configurations;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Security;

#pragma warning disable CS8629 // Nullable value type may be null.

namespace Tasks.Api.Controllers;

/// <summary>
/// URL Prefix: /completions
/// </summary>
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
    /// PUT: /completions/:eventId/:onDate
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    [HttpPut("{eventId}/{onDate}")]
    public async Task<ActionResult<EventAction>> CreateCompletionAsync([FromRoute] Guid eventId, [FromRoute] DateTime onDate)
    {
        // make sure user owns the event before marking it complete
        var clientOwnsEvent = await _eventServices.ClientOwnsEventAsync(eventId, CurrentUserId);
        if (!clientOwnsEvent)
        {
            return NotFound();
        }

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
        // make sure user owns the event before marking it complete
        var clientOwnsEvent = await _eventServices.ClientOwnsEventAsync(eventId, CurrentUserId);
        if (!clientOwnsEvent)
        {
            return NotFound();
        }


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
        // make sure user owns the event before marking it complete
        var clientOwnsEvent = await _eventServices.ClientOwnsEventAsync(eventId, CurrentUserId);

        if (!clientOwnsEvent)
        {
            return NotFound();
        }

        var eventCompletion = await _eventCompletionServices.GetEventCompletionAsync(eventId, onDate);

        if (eventCompletion == null)
        {
            return NotFound();  // return not found if there is no completion recorded for this event on this day
        }

        return Ok(eventCompletion);
    }

}
