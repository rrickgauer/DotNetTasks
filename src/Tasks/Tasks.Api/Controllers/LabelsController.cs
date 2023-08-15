using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Configurations;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Security;
using Tasks.Service.Auth;
using Tasks.Api.Controllers.Bases;

namespace Tasks.Api.Controllers;


[Authorize]
[ApiController]
[Route("labels")]
public class LabelsController : AuthorizedControllerBase
{
    #region Private members
    private readonly IConfigs _configs;
    private readonly ILabelServices _labelServices;
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
        var labels = await _labelServices.GetLabelsAsync(CurrentUserId);

        return Ok(labels);
    }


    /// <summary>
    /// GET: /labels/:labelId
    /// </summary>
    /// <returns></returns>
    [HttpGet("{labelId}")]
    [ServiceFilter(typeof(LabelAuthFilter))]
    public async Task<ActionResult<Label>> Get([FromRoute] Guid labelId)
    {
        //var label = await _labelServices.GetLabelAsync(labelId, CurrentUserId);
        var label = await _labelServices.GetLabelAsync(labelId);

        return Ok(label);
    }



    /// <summary>
    /// PUT: /labels/:labelId
    /// </summary>
    /// <returns></returns>
    [HttpPut("{labelId}")]
    public async Task<ActionResult<Label>> Put([FromRoute] Guid labelId, [FromForm] UpdateLabelForm form)
    {
        Label label = new()
        {
            Id = labelId,
            UserId = CurrentUserId,
            CreatedOn = DateTime.Now,
        };

        form.CopyFieldsToModel(label);


        var updatedLabel = await _labelServices.SaveLabelAsync(label);

        if (updatedLabel == null)
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
    [ServiceFilter(typeof(LabelAuthFilter))]
    public async Task<IActionResult> Delete([FromRoute] Guid labelId)
    {
        await _labelServices.DeleteLabelAsync(labelId);

        return NoContent();
    }

}
