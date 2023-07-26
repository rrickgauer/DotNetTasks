using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Controllers.Bases;
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
    public async Task<ActionResult<IEnumerable<Checklist>>> GetChecklistsAsync()
    {
        var lists = await _checklistServices.GetUserChecklistsAsync(CurrentUserId);

        return Ok(lists);
    }


    /// <summary>
    /// GET: /checklists/:checklistId
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    [HttpGet("{checklistId}")]
    public async Task<ActionResult<Checklist>> GetChecklistAsync(Guid checklistId)
    {
        var checklist = await _checklistServices.GetChecklistAsync(checklistId);

        if (checklist == null)
        {
            return NotFound();
        }

        if (checklist.UserId != CurrentUserId)
        {
            return Forbid();
        }

        return Ok(checklist);
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
        if (status == ModifyChecklistStatus.CanCreate)
        {
            return Created($"/checklists/{result.Id}", result);
        }

        return Ok(result);
    }


}
