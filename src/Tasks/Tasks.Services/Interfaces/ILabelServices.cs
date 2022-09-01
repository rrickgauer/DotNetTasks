

using static Tasks.Domain.Responses.ServicesResponses.LabelServicesResponses;

namespace Tasks.Services.Interfaces;


public interface ILabelServices
{
    public Task<GetLabelsResponse> GetLabelsAsync(Guid userId);
}
