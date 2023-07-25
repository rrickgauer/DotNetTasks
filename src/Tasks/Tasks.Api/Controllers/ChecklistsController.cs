using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Api.Controllers.Bases;
using Tasks.Service.Domain.Models;
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
        return Ok();
    }



}
