using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Security;
using Tasks.Services.Interfaces;

namespace Tasks.Controllers;

/// <summary>
/// Url Prefix: /event/event:
/// </summary>
[Authorize]
[ApiController]
[Route("events")]
public class EventLabelsController : ControllerBase
{
    #region Private members
    private readonly IConfigs _configuration;
    private readonly IEventLabelServices _eventLabelServices;
    private Guid CurrentUserId => SecurityMethods.GetUserIdFromRequest(Request).Value;
    #endregion


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    public EventLabelsController(IConfigs configuration, IEventLabelServices eventLabelServices)
    {
        _configuration = configuration;
        _eventLabelServices = eventLabelServices;
    }

    /// <summary>
    /// PUT: /events/:eventId/labels/:labelId
    /// </summary>
    /// <param name="eventLabelRequest">Url parms</param>
    /// <returns></returns>
    [HttpPut("{eventId}/labels/{labelId}")]
    public async Task<ActionResult<EventLabel>> Put([FromRoute] EventLabelRequestParms eventLabelRequest)
    {
        return Ok(eventLabelRequest);
    }

}
