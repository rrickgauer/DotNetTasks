using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Configurations;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Security;

namespace Tasks.Api.Controllers;

[Authorize]
[ApiController]
[Route("password")]
public class PasswordController : ControllerBase
{
    #region Private members
    private readonly IConfigs _configuration;
    private readonly IUserServices _userServices;
    private Guid CurrentUserId => SecurityMethods.GetUserIdFromRequest(Request).Value;
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="eventServices"></param>
    public PasswordController(IConfigs configuration, IUserServices eventServices)
    {
        _configuration = configuration;
        _userServices = eventServices;
    }


    /// <summary>
    /// PUT: /password
    /// </summary>
    /// <param name="eventFromBody"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> UpdatePassword([FromForm] UpdatePasswordForm updatePassword)
    {
        var updated = await _userServices.UpdatePasswordAsync(CurrentUserId, updatePassword.Password);

        if (!updated)
        {
            throw new Exception("FUcked up");
        }

        return Ok();
    }

}
