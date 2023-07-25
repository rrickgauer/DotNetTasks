using Tasks.Service.Domain.Parms;
using static Tasks.Service.Domain.Responses.Basic.LabelServicesResponses;

namespace Tasks.Service.Services.Interfaces;

public interface ILabelServices
{
    public Task<GetLabelsResponse> GetLabelsAsync(Guid userId);
    public Task<GetLabelResponse> GetLabelAsync(Guid labelId, Guid userId);
    public Task<ModifyLabelResponse> UpdateLabelAsync(Guid labelId, Guid userId, UpdateLabelForm updateLabelForm);
    public Task<DeleteLabelResponse> DeleteLabelAsync(Guid labelId, Guid userId);
    public Task<bool> ClientOwnsLabelsAsync(Guid userId, IEnumerable<Guid> labelIds);
}
