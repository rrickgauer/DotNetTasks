using System.Data;
using Tasks.Service.Domain.Models;

namespace Tasks.Service.Repositories.Interfaces;

public interface ILabelRepository
{
    public Task<DataTable> SelectLabelsAsync(Guid userId);
    public Task<int> ModifyLabelAsync(Label label);
    public Task<DataRow?> SelectLabelAsync(Guid labelId);
    public Task<int> DeleteLabelAsync(Label label);
}
