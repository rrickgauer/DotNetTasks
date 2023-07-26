/***********************************************************************
 
This filter ensures that the client has authorization for viewing a Checklist.
 
************************************************************************/

using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Tasks.Service.Errors;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Utilities;

namespace Tasks.Service.Auth;

public class ChecklistAuthFilters : IAsyncActionFilter
{
    private readonly IChecklistServices _checklistServices;

    public ChecklistAuthFilters(IChecklistServices checklistServices)
    {
        _checklistServices = checklistServices;
    }


    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var clientId = FilterUtitilities.GetClientId(context);
        var checklistId = FilterUtitilities.GetRequestRouteValue<Guid>(context, "checklistId");

        var checklist = await _checklistServices.GetChecklistAsync(checklistId);

        // does checklist exist?
        if (checklist == null)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // is client authorized to view it?
        if (checklist.UserId != clientId)
        {
            throw new HttpResponseException(HttpStatusCode.Forbidden);
        }

        await next();
    }
}
