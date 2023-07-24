using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Domain.Views;

namespace Tasks.Service.Services.Interfaces;

public interface IEventLabelServices
{
    Task<EventLabel?> CreateAsync(EventLabelRequestParms eventLabelRequestParms, Guid userId);
    Task<IEnumerable<Label>> GetEventLabelsAsync(Guid eventId, Guid userId);
    string GetUri(EventLabel eventLabel);
    Task<int> CreateBatchAsync(EventLabelsBatchRequest eventLabelsBatchRequest);
    Task<IEnumerable<EventLabel>> GetUserEventLabelsAsync(Guid userId);
    Task<IEnumerable<LabelAssignment>> GetUserEventLabelAssignmentsAsync(Guid eventId, Guid userId);

    Task<bool> DeleteAsync(Guid eventId, Guid labelId);
}
