using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Domain.Responses.Custom;
using Tasks.Service.Domain.TableView;

namespace Tasks.Service.Services.Interfaces;

public interface IChecklistLabelServices
{
    public Task<IEnumerable<ChecklistLabelView>> GetChecklistLabelsAsync(Guid userId);
    public Task<IEnumerable<ChecklistView>> GetAssignedChecklistsAsync(Guid labelId);
    public Task<IEnumerable<Label>> GetAssignedLabelsAsync(Guid checklistId);
    public Task<IEnumerable<LabelAssignment>> GetLabelAssignmentsForChecklist(Guid checklistId, Guid userId);
    public Task<int> SaveAsync(ChecklistLabel checklistLabel);
    public Task<int> DeleteAsync(ChecklistLabelForm assignment);
}
