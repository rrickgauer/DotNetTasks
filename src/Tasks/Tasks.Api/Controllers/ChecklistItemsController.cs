﻿using Microsoft.AspNetCore.Authorization;
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
[Route("checklists/{checklistId}/items")]
[ServiceFilter(typeof(ChecklistAuthFilters))]   // ensure client is authorized to modify checklist and its items
public class ChecklistItemsController : AuthorizedControllerBase
{
    #region - Private members -
    private readonly IChecklistItemServices _checklistItemServices;
    #endregion

    public ChecklistItemsController(IChecklistItemServices checklistItemServices)
    {
        _checklistItemServices = checklistItemServices;
    }


    /// <summary>
    /// GET: /checklists/:checklistId/items
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ChecklistItem>> GetChecklistItemsAsync([FromRoute] Guid checklistId)
    {
        var items = await _checklistItemServices.GetChecklistItemsAsync(checklistId);

        return Ok(items);
    }

    /// <summary>
    /// GET: /checklists/:checklistId/items/:itemId
    /// </summary>
    /// <param name="checklistId"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [HttpGet("{itemId}")]
    [ServiceFilter(typeof(ChecklistItemAuthFilter))]
    public async Task<ActionResult<ChecklistItem>> GetChecklistItemAsync([FromRoute] Guid checklistId, [FromRoute] Guid itemId)
    {
        var item = await _checklistItemServices.GetChecklistItemAsync(itemId);

        return Ok(item);
    }


    /// <summary>
    /// POST: /checklists/:checklistId/items
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostChecklistItemAsync([FromRoute] Guid checklistId, [FromForm] ModifyChecklistItemForm modifyChecklistItemForm)
    {
        ChecklistItem newItem = new()
        {
            Id = Guid.NewGuid(),
            ChecklistId = checklistId,
        };

        modifyChecklistItemForm.CopyFieldsToModel(newItem);

        await _checklistItemServices.SaveChecklistItemAsync(newItem);

        string uri = $"/checklists/{checklistId}/items/{newItem.Id}";
        return Created(uri, newItem);
    }


    /// <summary>
    /// PUT: /checklists/:checklistId/items/:itemId
    /// </summary>
    /// <param name="checklistId"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [HttpPut("{itemId}")]
    public async Task<IActionResult> PutChecklistItemAsync([FromRoute] Guid checklistId, [FromRoute] Guid itemId)
    {
        return Ok("put");
    }


    /// <summary>
    /// DELETE: /checklists/:checklistId/items/:itemId
    /// </summary>
    /// <param name="checklistId"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [HttpDelete("{itemId}")]
    [ServiceFilter(typeof(ChecklistItemAuthFilter))]
    public async Task<IActionResult> DeleteChecklistItemAsync([FromRoute] Guid checklistId, [FromRoute] Guid itemId)
    {
        return NoContent();
    }


}
