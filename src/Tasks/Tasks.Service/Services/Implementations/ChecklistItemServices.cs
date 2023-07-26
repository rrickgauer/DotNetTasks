using Tasks.Service.Domain.Models;
using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Services.Interfaces;

namespace Tasks.Service.Services.Implementations;

public class ChecklistItemServices : IChecklistItemServices
{

    #region - Private members - 
    private readonly IChecklistItemRepository _repository;
    private readonly IMapperServices _mapperServices;
    #endregion


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="mapperServices"></param>
    public ChecklistItemServices(IChecklistItemRepository repository, IMapperServices mapperServices)
    {
        _repository = repository;
        _mapperServices = mapperServices;
    }

    /// <summary>
    /// Get all the items in a checklist
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ChecklistItem>> GetChecklistItemsAsync(Guid checklistId)
    {
        var datatable = await _repository.SelectChecklistItemsAsync(checklistId);

        var items = _mapperServices.ToModels<ChecklistItem>(datatable);

        return items;
    }

    /// <summary>
    /// Get a the specified checklist item
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public async Task<ChecklistItem?> GetChecklistItemAsync(Guid itemId)
    {
        var row = await _repository.SelectChecklistItemAsync(itemId);

        if (row == null)
        {
            return null;
        }

        var checklistItem = _mapperServices.ToModel<ChecklistItem>(row);

        return checklistItem;
    }

    /// <summary>
    /// Save the given checklist item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public async Task<int> SaveChecklistItemAsync(ChecklistItem item)
    {
        var numRecords = await _repository.SaveChecklistItemAsync(item);

        return numRecords;
    }

    /// <summary>
    /// Delete the specified checklist item
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public async Task<int> DeleteChecklistItemAsync(Guid itemId)
    {
        return await _repository.DeleteChecklistItemAsync(itemId);
    }
}
