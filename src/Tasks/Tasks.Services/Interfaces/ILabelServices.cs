using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Domain.Responses;
using static Tasks.Domain.Responses.ServicesResponses.LabelServicesResponses;

namespace Tasks.Services.Interfaces;


public interface ILabelServices
{
    public Task<GetLabelsResponse> GetLabelsAsync(Guid userId);
    public Task<GetLabelResponse> GetLabelAsync(Guid labelId, Guid userId);
    public Task<ModifyLabelResponse> UpdateLabelAsync(Guid labelId, Guid userId, UpdateLabelForm updateLabelForm);
    public Task<DeleteLabelResponse> DeleteLabelAsync(Guid labelId, Guid userId);

}
