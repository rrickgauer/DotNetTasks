using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Security;

namespace Tasks.Controllers;


[Authorize]
[ApiController]
[Route("labels")]
public class LabelsController : ControllerBase
{
    #region Private members
    private readonly IConfigs _configs;
    //private readonly IEventServices _eventServices;
    private Guid CurrentUserId => SecurityMethods.GetUserIdFromRequest(Request).Value;
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    public LabelsController(IConfigs configuration)
    {
        _configs = configuration;
    }


    /// <summary>
    /// GET: /labels
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Label>>> GetAll()
    {
        return Ok("Get labels");
    }




}
