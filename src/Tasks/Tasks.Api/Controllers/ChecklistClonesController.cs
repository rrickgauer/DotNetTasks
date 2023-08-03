using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tasks.Service.Auth;
using Tasks.Service.Services.Interfaces;

namespace Tasks.Api.Controllers;

[Authorize]
[ApiController]
[Route("checklists/{checklistId}/clones")]
[ServiceFilter(typeof(ChecklistAuthFilters))]   // ensure client is authorized to modify checklist and its items
public class ChecklistClonesController : ControllerBase
{
    #region - Private members -
    private readonly IChecklistServices _checklistServices;
    #endregion


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="checklistItemServices"></param>
    public ChecklistClonesController(IChecklistServices checklistItemServices)
    {
        _checklistServices = checklistItemServices;
    }

    /// <summary>
    /// POST: /checklists/:checklistId/clones
    /// </summary>
    /// <param name="checklistId"></param>
    /// <param name="title"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CloneChecklistAsync([FromRoute] Guid checklistId, [FromForm] string title)
    {
        var newChecklist = await _checklistServices.CopyChecklistAsync(checklistId, title);

        var uri = $"/checklists/{newChecklist.Id}";

        return Created(uri, newChecklist);
    }

}
