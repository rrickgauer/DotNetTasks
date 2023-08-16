using Tasks.Service.Domain.Models;

namespace Tasks.Service.Services.Interfaces;

public interface ILabelServices
{
    public Task<IEnumerable<Label>> GetLabelsAsync(Guid userId);
    public Task<Label?> GetLabelAsync(Guid labelId);
    public Task<Label?> SaveLabelAsync(Label label);
    public Task<int> DeleteLabelAsync(Guid labelId);
    public Task<bool> ClientOwnsLabelsAsync(Guid userId, IEnumerable<Guid> labelIds);
}
