using System.Data;
using Tasks.Service.Domain.Models;

namespace Tasks.Service.Repositories.Interfaces;

public interface IChecklistItemRepository
{
    public Task<DataTable> SelectChecklistItemsAsync(Guid checklistId);
    public Task<DataRow?> SelectChecklistItemAsync(Guid itemId);
    public Task<int> SaveChecklistItemAsync(ChecklistItem checklistItem);
    public Task<int> DeleteChecklistItemAsync(Guid itemId);
    public Task<int> CopyChecklistItemsAsync(Guid sourceChecklistId, Guid targetChecklistId);

    public Task<DataTable> SelectChecklistItemViewsAsync(Guid checklistId);
    public Task<DataTable> SelectChecklistItemViewsByCliIdAsync(uint checklistCommandLineReferenceId);
    public Task<DataRow?> SelectByCommandLinereferenceAsync(uint commandLineReference);
}
    