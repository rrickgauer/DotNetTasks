/***********************************************************************
 
This filter ensures that a checklist item exists.
 
************************************************************************/

using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Tasks.Service.Errors;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Utilities;

namespace Tasks.Service.Auth;

public class ChecklistItemAuthFilter : IAsyncActionFilter
{
    private readonly IChecklistItemServices _checklistItemService;

    public ChecklistItemAuthFilter(IChecklistItemServices checklistServices)
    {
        _checklistItemService = checklistServices;
    }


    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var itemId = FilterUtitilities.GetRequestRouteValue<Guid>(context, "itemId");
        var checklistId = FilterUtitilities.GetRequestRouteValue<Guid>(context, "checklistId");

        var checklistItem = await _checklistItemService.GetChecklistItemAsync(itemId);

        if (checklistItem == null)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        if (checklistItem.ChecklistId != checklistId)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        await next();
    }
}
