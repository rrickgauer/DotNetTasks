using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.TableView;

namespace Tasks.Service.Services.Interfaces;

public interface IChecklistItemServices
{
    public Task<IEnumerable<ChecklistItem>> GetChecklistItemsAsync(Guid checklistId);
    public Task<ChecklistItem?> GetChecklistItemAsync(Guid itemId);
    public Task<int> SaveChecklistItemAsync(ChecklistItem item);
    public Task<int> DeleteChecklistItemAsync(Guid itemId);
    public Task<int> CopyChecklistItemsAsync(Guid sourceId, Guid destinationId);
    public Task<ChecklistItem?> MarkItemCompleteAsync(Guid itemId);
    public Task<ChecklistItem?> MarkItemIncompleteAsync(Guid itemId);
    public Task<string> GetExportItemsStringAsync(Guid checklistId);

    public Task<IEnumerable<ChecklistItemView>> GetChecklistItemViewsAsync(Guid checklistId);
    public Task<IEnumerable<ChecklistItemView>> GetItemsByChecklistCliReferenceAsync(uint checklistCommandLineReferenceId);


}
