using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Controllers.Bases;
using Tasks.Service.Auth;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Services.Interfaces;

namespace Tasks.Api.Controllers;

[Authorize]
[ApiController]
[Route("checklists")]
public class ChecklistsController : AuthorizedControllerBase
{
    #region - Private Members -
    private readonly IChecklistServices _checklistServices;
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="checklistServices"></param>
    public ChecklistsController(IChecklistServices checklistServices)
    {
        _checklistServices = checklistServices;
    }

    /// <summary>
    /// GET: /checklists
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    //public async Task<ActionResult<IEnumerable<Checklist>>> GetChecklistsAsync()
    public async Task<IActionResult> GetChecklistsAsync()
    {
        try
        {
            var lists = await _checklistServices.GetUserChecklistsAsync(CurrentUserId);
            return Ok(lists);
        }
        catch(Exception ex)
        {
            return BadRequest(ex);
        }

    }


    /// <summary>
    /// GET: /checklists/:checklistId
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    [HttpGet("{checklistId}")]
    [ServiceFilter(typeof(ChecklistAuthFilters))]
    public async Task<ActionResult<Checklist>> GetChecklistAsync(Guid checklistId)
    {
        try
        {
            var checklist = await _checklistServices.GetChecklistAsync(checklistId);
            return Ok(checklist);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
       
    }


    /// <summary>
    /// PUT: /checklists/:checklistId
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    [HttpPut("{checklistId}")]
    public async Task<ActionResult<Checklist>> PutChecklistAsync([FromRoute] Guid checklistId, [FromForm] ModifyChecklistForm modifyChecklistForm)
    {
        // make sure the client is authorized to modify the checklist
        var status = await _checklistServices.GetModifyChecklistStatusAsync(checklistId, CurrentUserId);

        if (status == ModifyChecklistStatus.TakenByAnotherUser)
        {
            return Forbid();
        }

        // copy over the form data into a new checklist model
        Checklist checklist = new()
        {
            Id = checklistId,
            UserId = CurrentUserId,
        };

        modifyChecklistForm.CopyFieldsToModel(checklist);

        // save the changes in the datbase
        var result = await _checklistServices.SaveChecklistAsync(checklist);

        // return the updated model
        if (status == ModifyChecklistStatus.Insert)
        {
            return Created($"/checklists/{result.Id}", result);
        }

        return Ok(result);
    }


    /// <summary>
    /// POST: /checklists
    /// </summary>
    /// <param name="modifyChecklistForm"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Checklist>> PostChecklistAsync([FromForm] ModifyChecklistForm modifyChecklistForm)
    {
        // copy over the form data into a new checklist model
        Checklist checklist = new()
        {
            Id = Guid.NewGuid(),
            UserId = CurrentUserId,
        };

        modifyChecklistForm.CopyFieldsToModel(checklist);

        // save the changes in the datbase
        var result = await _checklistServices.SaveChecklistAsync(checklist);

        return Created($"/checklists/{result.Id}", result);
    }

    /// <summary>
    /// DELETE: /checklists/:checklistId
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    [HttpDelete("{checklistId}")]
    [ServiceFilter(typeof(ChecklistAuthFilters))]
    public async Task<IActionResult> DeleteChecklistAsync([FromRoute] Guid checklistId)
    {
        await _checklistServices.DeleteChecklistAsync(checklistId);

        return NoContent();
    }


}
