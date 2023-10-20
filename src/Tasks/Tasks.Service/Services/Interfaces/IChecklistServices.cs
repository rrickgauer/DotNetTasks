using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.TableView;

namespace Tasks.Service.Services.Interfaces;

public interface IChecklistServices
{
    public Task<IEnumerable<ChecklistView>> GetUserChecklistsAsync(Guid userId);
    public Task<ChecklistView?> GetChecklistAsync(Guid checklistId);
    public Task<ModifyChecklistStatus> GetModifyChecklistStatusAsync(Guid checklistId, Guid userId);
    public Task<ChecklistView> SaveChecklistAsync(Checklist checklist);
    public Task<int> DeleteChecklistAsync(Guid checklistId);
    public Task<ChecklistView> CopyChecklistAsync(Guid existingChecklistId, string newChecklistTitle);

    public Task<ChecklistView?> GetChecklistByCliReferenceAsync(uint commandLineReference);
}




