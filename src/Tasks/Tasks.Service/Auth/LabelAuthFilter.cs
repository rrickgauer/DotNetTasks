using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Tasks.Service.Errors;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Utilities;

namespace Tasks.Service.Auth;

public class LabelAuthFilter : IAsyncActionFilter
{

    private readonly ILabelServices _labelServices;

    public LabelAuthFilter(ILabelServices labelServices)
    {
        _labelServices = labelServices;
    }


    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var clientId = FilterUtitilities.GetClientId(context);
        var labelId = FilterUtitilities.GetLabelIdRouteValue(context);

        var label = (await _labelServices.GetLabelAsync(labelId, clientId)).Data;

        if (label == null)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        await next();
    }
}
