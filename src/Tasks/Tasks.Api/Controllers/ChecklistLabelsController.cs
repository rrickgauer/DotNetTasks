using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Controllers.Bases;
using Tasks.Service.Auth;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Services.Interfaces;

namespace Tasks.Api.Controllers;

[Authorize]
[ApiController]
[Route("checklists/{checklistId}/labels")]
[ServiceFilter(typeof(ChecklistAuthFilters))]   // ensure client is authorized to modify checklist and its items
public class ChecklistLabelsController : AuthorizedControllerBase
{
    #region - Private Members -
    private readonly IChecklistLabelServices _checklistLabelServices;
    #endregion


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="services"></param>
    public ChecklistLabelsController(IChecklistLabelServices services)
    {
        _checklistLabelServices = services;
    }

    /// <summary>
    /// GET: /checklists/:checklistId/labels
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Label>>> GetAllAsync([FromRoute] Guid checklistId)
    {
        var assignedLabels = await _checklistLabelServices.GetAssignedLabelsAsync(checklistId);

        return Ok(assignedLabels);
    }

    /// <summary>
    /// PUT: /checklists/:checklistId/labels/:labelId
    /// </summary>
    /// <param name="checklistId"></param>
    /// <param name="labelId"></param>
    /// <returns></returns>
    [HttpPut("{labelId}")]
    [ServiceFilter(typeof(LabelAuthFilter))]
    public async Task<ActionResult<ChecklistLabel>> PutAsync([FromRoute] Guid checklistId, [FromRoute] Guid labelId)
    {
        ChecklistLabel checklistLabel = new()
        {
            ChecklistId = checklistId,
            LabelId = labelId,
            CreatedOn = DateTime.Now,
        };

        await _checklistLabelServices.SaveAsync(checklistLabel);

        return Ok(checklistLabel);
    }

    /// <summary>
    /// DELETE: /checklists/:checklistId/labels/:labelId
    /// </summary>
    /// <param name="checklistId"></param>
    /// <param name="labelId"></param>
    /// <returns></returns>
    [HttpDelete("{labelId}")]
    [ServiceFilter(typeof(LabelAuthFilter))]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid checklistId, [FromRoute] Guid labelId)
    {
        ChecklistLabelForm checklistLabel = new()
        {
            ChecklistId = checklistId,
            LabelId = labelId
        };

        var numrecords = await _checklistLabelServices.DeleteAsync(checklistLabel);

        if (numrecords == 0)
        {
            return NotFound();
        }

        return NoContent();

    }
}
