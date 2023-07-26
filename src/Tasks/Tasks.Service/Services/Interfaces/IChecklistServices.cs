using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.TableView;

namespace Tasks.Service.Services.Interfaces;

public interface IChecklistServices
{
    public Task<IEnumerable<ChecklistTableView>> GetUserChecklistsAsync(Guid userId);
    public Task<ChecklistTableView?> GetChecklistAsync(Guid checklistId);
    public Task<ModifyChecklistStatus> GetModifyChecklistStatusAsync(Guid checklistId, Guid userId);
    public Task<ChecklistTableView> SaveChecklistAsync(Checklist checklist);
}




