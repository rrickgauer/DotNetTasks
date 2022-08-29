/***********************************************************************
 
This class is responsible for ensuring that the incoming request has the custom header key/value combination.

This is to ensure that the request is coming only from the gui and not from a 3rd party.

To utilitize this attribute on a controller endpoint:
    
    [ServiceFilter(typeof(CustomHeaderFilter))]
 
************************************************************************/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Tasks.Configurations;

namespace Tasks.Auth
{
    public class CustomHeaderFilter : ActionFilterAttribute
    {
        #region Private members
        private readonly IConfigs _configs;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configs"></param>
        public CustomHeaderFilter(IConfigs configs)
        {
            _configs = configs;
        }

        /// <summary>
        /// Steps to take before the controller endpoint method is executed.
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(_configs.REQUEST_HEADER_KEY, out StringValues value))
            {
                context.Result = new UnauthorizedObjectResult("Missing custom request header key.");
            }

            if (!value.Contains(_configs.REQUEST_HEADER_VALUE))
            {
                context.Result = new UnauthorizedObjectResult("Invalid custom request header value.");
            }
        }
    }
}
