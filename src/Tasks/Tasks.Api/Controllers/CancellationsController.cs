﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Service.Domain.Models;
using Tasks.Service.Configurations;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Security;
using Tasks.Service.Auth;

namespace Tasks.Api.Controllers;

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
    [ServiceFilter(typeof(EventAuthFilter))]
    public async Task<ActionResult<EventAction>> CreateCancellationAsync([FromRoute] Guid eventId, [FromRoute] DateTime onDate)
    {
        // save it to the database
        var newCompletion = await _eventCompletionServices.SaveEventCancellationAsync(eventId, onDate);

        return Ok(newCompletion);
    }


}
