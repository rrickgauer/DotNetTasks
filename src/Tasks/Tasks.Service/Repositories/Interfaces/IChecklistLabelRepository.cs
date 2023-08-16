using System.Data;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;

namespace Tasks.Service.Repositories.Interfaces;

public interface IChecklistLabelRepository
{
    public Task<DataTable> SelectAllLabelsAssignedToChecklistAsync(Guid checklistId);
    public Task<int> ModifyAsync(ChecklistLabel checklistLabel);
    public Task<int> DeleteAsync(ChecklistLabelForm checklistLabel);
}
