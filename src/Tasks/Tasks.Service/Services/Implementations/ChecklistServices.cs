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

    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="checklistRepository"></param>
    /// <param name="modelMapperServices"></param>
    public ChecklistServices(IChecklistRepository checklistRepository, IMapperServices modelMapperServices)
    {
        _checklistRepository = checklistRepository;
        _mapperServices = modelMapperServices;
    }

    /// <summary>
    /// Get all the specified users checklists
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ChecklistTableView>> GetUserChecklistsAsync(Guid userId)
    {
        var datatable = await _checklistRepository.SelectUserChecklistsAsync(userId);

        var checklists = _mapperServices.ToModels<ChecklistTableView>(datatable);

        return checklists;
    }

    /// <summary>
    /// Get a single checklist
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    public async Task<ChecklistTableView?> GetChecklistAsync(Guid checklistId)
    {
        var datarow = await _checklistRepository.SelectChecklistAsync(checklistId);

        if (datarow == null)
            return null;

        var model = _mapperServices.ToModel<ChecklistTableView>(datarow);

        return model;
    }

    public async Task<ModifyChecklistStatus> GetModifyChecklistStatusAsync(Guid checklistId, Guid userId)
    {
        var checklist = await GetChecklistAsync(checklistId);

        if (checklist is null)
        {
            return ModifyChecklistStatus.CanCreate;
        }

        if (checklist.UserId != userId)
        {
            return ModifyChecklistStatus.TakenByAnotherUser;
        }

        return ModifyChecklistStatus.CanUpdate;
    }


    public async Task<ChecklistTableView> SaveChecklistAsync(Checklist checklist)
    {
        // save the changes
        await _checklistRepository.SaveChecklistAsync(checklist);

        // return the updated checklist
        return await GetChecklistAsync(checklist.Id.Value);
    }
}




