using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Auth;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Domain.Views;
using Tasks.Errors;
using Tasks.Security;
using Tasks.Services.Interfaces;

namespace Tasks.Api.Controllers;

[Authorize]
[ApiController]
[Route("user")]
public class UserController : ControllerBase
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
    /// <param name="userServices"></param>
    public UserController(IConfigs configuration, IUserServices userServices)
    {
        _configuration = configuration;
        _userServices = userServices;
    }

    /// <summary>
    /// POST: /user/signup
    /// 
    /// DOES NOT NEED AUTH HEADER!!
    /// 
    /// </summary>
    /// <param name="signUpRequest"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [ServiceFilter(typeof(CustomHeaderFilter))]
    [HttpPost("signup")]
    public async Task<ActionResult<SignupRequestResponse>> SignUp([FromForm] SignUpRequest signUpRequest)
    {
        // validate the user info before inserting it into the database
        var validationResult = await _userServices.ValidateNewUserAsync(signUpRequest);

        if (validationResult != Domain.Enums.ValidateUserResult.Valid)
        {
            return BadRequest(_userServices.GetInvalidSignUpRequestResponse(validationResult));
        }

        // insert it
        User? newUser = await _userServices.CreateUserAsync(signUpRequest);

        SignupRequestResponse result = new()
        {
            Successful = true,
            User = newUser,
        };

        return Created("/user", result);
    }

    /// <summary>
    /// GET: /user
    /// </summary>
    /// <returns></returns>
    [ServiceFilter(typeof(CustomHeaderFilter))]
    [HttpGet]
    public async Task<ActionResult<GetUserResponse>> GetUser()
    {
        GetUserResponse? response = await _userServices.GetUserViewAsync(CurrentUserId);

        if (response is null) return NotFound();

        return Ok(response);
    }


    /// <summary>
    /// PUT: /user
    /// These requests must come from GUI and not a third party.
    /// </summary>
    /// <returns></returns>
    [ServiceFilter(typeof(CustomHeaderFilter))]
    [HttpPut]
    public async Task<ActionResult<GetUserResponse>> Put([FromForm] UpdateUserRequestForm requestForm)
    {
        bool successfulUpdate = false;

        try
        {
            successfulUpdate = await _userServices.UpdateUserAsync(CurrentUserId, requestForm);
        }
        catch (TasksException tasksException)
        {
            return BadRequest(tasksException.Message);
        }

        if (!successfulUpdate)
        {
            return BadRequest("There was an error updating the user.");
        }

        GetUserResponse? getUserResponse = await _userServices.GetUserViewAsync(CurrentUserId);

        if (getUserResponse is null) return NotFound();

        return Ok(getUserResponse);
    }

}
