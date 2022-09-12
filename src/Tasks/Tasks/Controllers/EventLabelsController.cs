using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Security;
using Tasks.Services.Interfaces;

namespace Tasks.Controllers;

using LabelsCollection = ActionResult<IEnumerable<Label>>;

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
    private readonly IEventServices _eventServices;
    private readonly ILabelServices _labelServices;
    private Guid CurrentUserId => SecurityMethods.GetUserIdFromRequest(Request).Value;
    #endregion


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    public EventLabelsController(IConfigs configuration, IEventLabelServices eventLabelServices, IEventServices eventServices, ILabelServices labelServices)
    {
        _configuration = configuration;
        _eventLabelServices = eventLabelServices;
        _eventServices = eventServices;
        _labelServices = labelServices;
    }

    /// <summary>
    /// PUT: /events/:eventId/labels/:labelId
    /// </summary>
    /// <param name="eventLabelRequest">Url parms</param>
    /// <returns></returns>
    [HttpPut("{eventId}/labels/{labelId}")]
    public async Task<ActionResult<EventLabel>> PutAsync([FromRoute] EventLabelRequestParms eventLabelRequest)
    {
        EventLabel? newEventLabel = await _eventLabelServices.CreateAsync(eventLabelRequest, CurrentUserId);
        
        if (newEventLabel is null)
        {
            return NotFound();
        }

        string url = _eventLabelServices.GetUri(newEventLabel); 

        return Created(url, newEventLabel);
    }

    /// <summary>
    /// GET: /events/:eventId/labels
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    [HttpGet("{eventId}/labels")]
    public async Task<LabelsCollection> GetAsync([FromRoute] Guid eventId)
    {
        // make sure the client owns the event
        var clientOwnsEvent = await _eventServices.ClientOwnsEventAsync(eventId, CurrentUserId);
        
        if (!clientOwnsEvent)
        {
            return NotFound();
        }

        // now, fetch the labels that have been assigned to the event
        var labels = await _eventLabelServices.GetEventLabelsAsync(eventId, CurrentUserId);

        return Ok(labels);
    }


    /// <summary>
    /// PUT: /events/:eventId/labels
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="labelIds"></param>
    /// <returns></returns>
    [HttpPut("{eventId}/labels")]
    public async Task<LabelsCollection> BatchPutAsync([FromRoute] Guid eventId, [FromBody] IEnumerable<Guid> labelIds)
    {
        // make sure the client owns the event
        var clientOwnsEvent = await _eventServices.ClientOwnsEventAsync(eventId, CurrentUserId);

        if (!clientOwnsEvent)
        {
            return NotFound();
        }

        // make sure the client owns all of the given label ids
        var labelsAreValid = await _labelServices.ClientOwnsLabelsAsync(CurrentUserId, labelIds);

        if (!labelsAreValid)
        {
            return BadRequest("Unknown label id given.");
        }

        // insert the labels into the database
        EventLabelsBatchRequest eventLabelsBatchRequest = new(eventId, CurrentUserId, labelIds);
        var insertResult = await _eventLabelServices.CreateBatchAsync(eventLabelsBatchRequest);

        // now, fetch the newly assigned labels
        var labels = await _eventLabelServices.GetEventLabelsAsync(eventId, CurrentUserId);

        return Ok(labels);
    }



}
