using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;

namespace Tasks.Service.Services.Interfaces;

public interface ILabelServices
{
    public Task<IEnumerable<Label>> GetLabelsAsync(Guid userId);
    public Task<Label?> GetLabelAsync(Guid labelId, Guid userId);
    public Task<Label?> SaveLabelAsync(Guid labelId, Guid userId, UpdateLabelForm updateLabelForm);
    public Task<int> DeleteLabelAsync(Guid labelId, Guid userId);
    public Task<bool> ClientOwnsLabelsAsync(Guid userId, IEnumerable<Guid> labelIds);
}
