#pragma warning disable CS8602 // Dereference of a possibly null reference.

using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Services.Interfaces;

namespace Tasks.Service.Services.Implementations;

public class ChecklistServices : IChecklistServices
{

    #region - Private members -
    private readonly IChecklistRepository _checklistRepository;
    private readonly IMapperServices _mapperServices;
    private readonly IChecklistItemServices _checklistItemServices;
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="checklistRepository"></param>
    /// <param name="modelMapperServices"></param>
    public ChecklistServices(IChecklistRepository checklistRepository, IMapperServices modelMapperServices, IChecklistItemServices checklistItemServices)
    {
        _checklistRepository = checklistRepository;
        _mapperServices = modelMapperServices;
        _checklistItemServices = checklistItemServices;
    }


    public async Task<ChecklistView?> GetChecklistByCliReferenceAsync(uint commandLineReference)
    {
        var row = await _checklistRepository.SelectChecklistByCommandLineReferenceAsync(commandLineReference);

        if (row == null)
        {
            return null;
        }

        return _mapperServices.ToModel<ChecklistView>(row);
    }

    /// <summary>
    /// Get all the specified users checklists
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ChecklistView>> GetUserChecklistsAsync(Guid userId)
    {
        var datatable = await _checklistRepository.SelectUserChecklistsAsync(userId);

        var checklists = _mapperServices.ToModels<ChecklistView>(datatable);

        return checklists;
    }

    /// <summary>
    /// Get a single checklist
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    public async Task<ChecklistView?> GetChecklistAsync(Guid checklistId)
    {
        var datarow = await _checklistRepository.SelectChecklistAsync(checklistId);

        if (datarow == null)
            return null;

        var model = _mapperServices.ToModel<ChecklistView>(datarow);

        return model;
    }

    /// <summary>
    /// Get the status for when a user is trying to modify a checklist.
    /// It can be:
    ///     - Inserted
    ///     - Updated
    ///     - Taken by another user
    /// </summary>
    /// <param name="checklistId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<ModifyChecklistStatus> GetModifyChecklistStatusAsync(Guid checklistId, Guid userId)
    {
        var checklist = await GetChecklistAsync(checklistId);

        if (checklist is null)
        {
            return ModifyChecklistStatus.Insert;
        }

        if (checklist.UserId != userId)
        {
            return ModifyChecklistStatus.TakenByAnotherUser;
        }

        return ModifyChecklistStatus.Update;
    }


    /// <summary>
    /// Save the specified checklist
    /// </summary>
    /// <param name="checklist"></param>
    /// <returns></returns>
    public async Task<ChecklistView> SaveChecklistAsync(Checklist checklist)
    {
        // save the changes
        await _checklistRepository.SaveChecklistAsync(checklist);

        // return the updated checklist
        return await GetChecklistAsync(checklist.Id.Value);
    }

    /// <summary>
    /// Delete the checklist
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    public async Task<int> DeleteChecklistAsync(Guid checklistId)
    {
        return await _checklistRepository.DeleteChecklistAsync(checklistId);
    }


    /// <summary>
    /// Create a copy of the checklist and its items
    /// </summary>
    /// <param name="existingChecklistId"></param>
    /// <param name="newChecklistTitle"></param>
    /// <returns></returns>
    public async Task<ChecklistView> CopyChecklistAsync(Guid existingChecklistId, string newChecklistTitle)
    {
        // create the new checklist first...
        var copiedChecklist = await CreateCopiedChecklistAsync(existingChecklistId, newChecklistTitle);

        // now, copy over the checklist items
        var countItems = await _checklistItemServices.CopyChecklistItemsAsync(existingChecklistId, copiedChecklist.Id.Value);

        copiedChecklist.CountItems = countItems;

        return copiedChecklist;
    }

    /// <summary>
    /// Create a copy of the specified checklist using the specified title.
    /// </summary>
    /// <param name="checklistId">The checklist to copy</param>
    /// <param name="title">The title of the new checklist</param>
    /// <returns></returns>
    private async Task<ChecklistView> CreateCopiedChecklistAsync(Guid checklistId, string title)
    {
        var existingChecklist = await GetChecklistAsync(checklistId);

        Checklist newChecklist = new()
        {
            Id = Guid.NewGuid(),
            Title = title,
            ListType = existingChecklist.ListType,
            UserId = existingChecklist.UserId,
            CreatedOn = DateTime.Now,
        };

        var result = await SaveChecklistAsync(newChecklist);

        return result;
    }

}




