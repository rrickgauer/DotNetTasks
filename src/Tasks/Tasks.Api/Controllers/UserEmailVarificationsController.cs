using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Auth;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Security;
using Tasks.Services.Interfaces;

namespace Tasks.Api.Controllers;

[Authorize]
[ApiController]
[Route("email-verifications")]
public class UserEmailVarificationsController : ControllerBase
{
    #region Private members
    private readonly IConfigs _configuration;
    private readonly IUserEmailVerificationServices _userEmailVerificationServices;
    private Guid CurrentUserId => SecurityMethods.GetUserIdFromRequest(Request).Value;
    #endregion


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="userEmailVerificationServices"></param>
    public UserEmailVarificationsController(IConfigs configuration, IUserEmailVerificationServices userEmailVerificationServices)
    {
        _configuration = configuration;
        _userEmailVerificationServices = userEmailVerificationServices;
    }

    [HttpPost]
    public async Task<ActionResult<UserEmailVerification>> Post()
    {
        // create the new record
        UserEmailVerification? emailVerification = await _userEmailVerificationServices.CreateNewAsync(CurrentUserId);

        if (emailVerification == null)
        {
            return BadRequest("Could not find the user...");
        }


        // send the email
        bool emailWasSent = await _userEmailVerificationServices.SendEmailAsync(emailVerification);

        return Created($"/email-verifications/{emailVerification.Id}", emailVerification);
    }


    [ServiceFilter(typeof(CustomHeaderFilter))]
    [AllowAnonymous]
    [HttpPut("{userEmailVerificationId}/confirm")]
    public async Task<IActionResult> Confirm([FromRoute] Guid userEmailVerificationId)
    {
        UserEmailVerification? result = await _userEmailVerificationServices.ConfirmEmailAsync(userEmailVerificationId);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
