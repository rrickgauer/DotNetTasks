#pragma warning disable CS8629 // Nullable value type may be null.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8603 // Possible null reference return.

using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Security;

namespace Tasks.Service.Utilities;

public static class FilterUtitilities
{
    /// <summary>
    /// Get the current client id 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static Guid GetClientId(ActionExecutingContext context)
    {
        return SecurityMethods.GetUserIdFromRequest(context.HttpContext.Request).Value;
    }



    public static Guid GetEventIdRouteValue(ActionExecutingContext context) => GetRequestRouteValue<Guid>(context, "eventId");


    /// <summary>
    /// Get the specified request value with the matching key.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T GetRequestRouteValue<T>(ActionExecutingContext context, string key)
    {
        var value = (T)context.ActionArguments[key];

        return value;
    }
}
