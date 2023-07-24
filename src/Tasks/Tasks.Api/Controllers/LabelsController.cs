using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Configurations;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Security;

namespace Tasks.Api.Controllers;


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
        var result = await _labelServices.GetLabelsAsync(CurrentUserId);

        if (!result.Successful)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }


    /// <summary>
    /// GET: /labels/:labelId
    /// </summary>
    /// <returns></returns>
    [HttpGet("{labelId}")]
    public async Task<ActionResult<Label>> Get([FromRoute] Guid labelId)
    {
        var result = await _labelServices.GetLabelAsync(labelId, CurrentUserId);

        if (!result.Successful)
        {
            return BadRequest(result);
        }

        if (result.Data is null)
        {
            return NotFound();
        }

        return Ok(result);
    }



    /// <summary>
    /// PUT: /labels/:labelId
    /// </summary>
    /// <returns></returns>
    [HttpPut("{labelId}")]
    public async Task<ActionResult<Label>> Put([FromRoute] Guid labelId, [FromForm] UpdateLabelForm form)
    {
        var updatedLabel = await _labelServices.UpdateLabelAsync(labelId, CurrentUserId, form);

        if (!updatedLabel.Successful)
        {
            return BadRequest(updatedLabel);
        }

        if (updatedLabel.Data is null)
        {
            return NotFound();
        }

        return Ok(updatedLabel);
    }


    /// <summary>
    /// DELETE: /labels/:labelId
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{labelId}")]
    public async Task<IActionResult> Delete([FromRoute] Guid labelId)
    {
        var deleteLabelResult = await _labelServices.DeleteLabelAsync(labelId, CurrentUserId);

        if (!deleteLabelResult.Successful)
        {
            return BadRequest(deleteLabelResult);
        }

        if (deleteLabelResult.Data is null)
        {
            return NotFound();
        }

        return NoContent();
    }




}
