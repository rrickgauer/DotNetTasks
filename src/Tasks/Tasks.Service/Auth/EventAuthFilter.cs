/***********************************************************************

- Ensures the event id has a matching record in the database.
- Ensures the client has authorization to access the event.
 
************************************************************************/

using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Tasks.Service.Errors;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Utilities;

namespace Tasks.Service.Auth;

public class EventAuthFilter : IAsyncActionFilter
{

    private readonly IEventServices _eventServices;

    public EventAuthFilter(IEventServices eventServices)
    {
        _eventServices = eventServices;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var clientId = FilterUtitilities.GetClientId(context);
        var eventId = FilterUtitilities.GetEventIdRouteValue(context);

        var eventRecord = await _eventServices.GetEventAsync(eventId);

        // does event exist?
        if (eventRecord == null)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // is client authorized to view it?
        if (eventRecord.UserId != clientId)
        {
            throw new HttpResponseException(HttpStatusCode.Forbidden);
        }

        await next();
    }
}
