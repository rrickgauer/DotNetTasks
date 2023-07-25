using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Service.Security;

namespace Tasks.Api.Controllers.Bases;


[ApiController]
public class AuthorizedControllerBase : ControllerBase
{
    protected Guid CurrentUserId => SecurityMethods.GetUserIdFromRequest(Request).Value;
}
