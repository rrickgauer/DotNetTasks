using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tasks.Service.Domain.Models;
using Tasks.Service.Repositories.Implementations;
using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Configurations;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Security;
using Tasks.Service.Auth;
using Tasks.Api.Controllers.Bases;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Parms;

namespace Tasks.Api.Controllers;

[Authorize]
[ApiController]
[Route("events")]
public class EventsController : AuthorizedControllerBase
{
    #region - Private members -
    private readonly IEventServices _eventServices;
    #endregion


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="eventRepository"></param>
    public EventsController(IEventServices eventServices)
    {
        _eventServices = eventServices;
    }


    /// <summary>
    /// GET: /events
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<Event>>> GetEventsAsync()
    {
        var userEvents = await _eventServices.GetUserEventsAsync(CurrentUserId);

        return Ok(userEvents);
    }


    /// <summary>
    /// GET: /events/:eventId
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    [HttpGet("{eventId}")]
    [ServiceFilter(typeof(EventAuthFilter))]
    public async Task<ActionResult<Event>> GetEventAsync(Guid eventId)
    {
        var e = await _eventServices.GetEventAsync(eventId);

        return Ok(e);
    }

    /// <summary>
    /// Delete: /events/:eventId
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    [HttpDelete("{eventId}")]
    [ServiceFilter(typeof(EventAuthFilter))]
    public async Task<IActionResult> DeleteEventAsync(Guid eventId)
    {
        await _eventServices.DeleteEventAsync(eventId);
        return NoContent();
    }

    /// <summary>
    /// PUT: /events/eventId
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="eventForm"></param>
    /// <returns></returns>
    [HttpPut("{eventId}")]
    public async Task<ActionResult<Event>> PutEventAsync(Guid eventId, [FromForm] ModifyEventForm eventForm)
    {
        // make sure client can update/create the event
        var putEventStatus = await _eventServices.GetPutEventStatusAsync(eventId, CurrentUserId);

        if (putEventStatus == PutEventStatus.Forbid)
        {
            return Forbid();
        }

        // copy over the request form data into an Event domain model
        Event model = new()
        {
            Id = eventId,
            UserId = CurrentUserId,
        };

        eventForm.CopyFieldsToModel(model);

        // save the event into the database
        await _eventServices.UpdateEventAsync(model);

        // get the complete event data
        var updatedEvent = await _eventServices.GetEventAsync(eventId);

        if (putEventStatus == PutEventStatus.Create)
        {
            return Created($"{Request.Path}", updatedEvent);    // created a new event
        }
        else
        {
            return Ok(updatedEvent);                            // updated an existing event
        }
    }

    /// <summary>
    /// POST: /events
    /// </summary>
    /// <param name="eventFromBody"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Event>> CreateEventAsync([FromForm] ModifyEventForm eventForm)
    {
        Event model = new()
        {
            Id = Guid.NewGuid(),
            UserId = CurrentUserId,
        };

        eventForm.CopyFieldsToModel(model);

        Event newEvent = await _eventServices.CreateNewEventAsync(model);

        // return it
        return Created($"{Request.Path}/{newEvent.Id}", newEvent);    // created a new event
    }

}
