using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Security;
using Tasks.Services.Interfaces;

namespace Tasks.Controllers;


[Authorize]
[ApiController]
[Route("labels")]
public class LabelsController : ControllerBase
{
    #region Private members
    private readonly IConfigs _configs;
    private readonly ILabelServices _labelServices;
    private Guid CurrentUserId => SecurityMethods.GetUserIdFromRequest(Request).Value;
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration"></param>
    public LabelsController(IConfigs configuration, ILabelServices labelServices)
    {
        _configs = configuration;
        _labelServices = labelServices;
    }


    /// <summary>
    /// GET: /labels
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Label>>> GetAll()
    {
        var getLabelsResponse = await _labelServices.GetLabelsAsync(CurrentUserId);

        return Ok(getLabelsResponse);
    }




}
