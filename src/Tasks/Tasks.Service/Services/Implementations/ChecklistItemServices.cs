using System.Runtime.InteropServices;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.TableView;
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
    /// Get the string for exporting items
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    public async Task<string> GetExportItemsStringAsync(Guid checklistId)
    {
        var items = await GetChecklistItemsAsync(checklistId);

        string result = string.Empty;

        foreach(var item in items) 
        {
            string check = item.IsComplete ? "x" : " ";
            result += $"- [{check}] {item.Content}{Environment.NewLine}";
        }

        return result;
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

    /// <summary>
    /// Copy over all the items from one checklist into another
    /// </summary>
    /// <param name="sourceChecklistId">The source checklist id</param>
    /// <param name="targetChecklistId">The destination checklist id</param>
    /// <returns></returns>
    public async Task<int> CopyChecklistItemsAsync(Guid sourceChecklistId, Guid targetChecklistId)
    {
        return await _repository.CopyChecklistItemsAsync(sourceChecklistId, targetChecklistId);
    }

    /// <summary>
    /// Mark the specified item as complete
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public async Task<ChecklistItem?> MarkItemCompleteAsync(Guid itemId)
    {
        return await SetItemComplete(itemId, true);
    }
    
    /// <summary>
    /// Mark the specified item as incomplete
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public async Task<ChecklistItem?> MarkItemIncompleteAsync(Guid itemId)
    {
        return await SetItemComplete(itemId, false);
    }


    /// <summary>
    /// Set the specified item's IsComplete value and save it.
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="isComplete"></param>
    /// <returns></returns>
    private async Task<ChecklistItem?> SetItemComplete(Guid itemId, bool isComplete)
    {
        var checklistItem = await GetChecklistItemAsync(itemId);

        if (checklistItem is null)
        {
            return null;
        }

        checklistItem.IsComplete = isComplete;

        await SaveChecklistItemAsync(checklistItem);

        return checklistItem;
    }


    public async Task<IEnumerable<ChecklistItemView>> GetChecklistItemViewsAsync(Guid checklistId)
    {
        var table = await _repository.SelectChecklistItemViewsAsync(checklistId);

        var items = _mapperServices.ToModels<ChecklistItemView>(table);

        return items;
    }

    public async Task<IEnumerable<ChecklistItemView>> GetItemsByChecklistCliReferenceAsync(uint checklistCommandLineReferenceId)
    {
        var table = await _repository.SelectChecklistItemViewsByCliIdAsync(checklistCommandLineReferenceId);

        var items = _mapperServices.ToModels<ChecklistItemView>(table);

        return items;
    }
}
