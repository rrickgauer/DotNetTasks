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

    public async Task<Checklist?> GetChecklistAsync(Guid checklistId)
    {
        throw new NotImplementedException();
    }
}




