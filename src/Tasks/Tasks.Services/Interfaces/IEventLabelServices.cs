using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Domain.Views;

namespace Tasks.Services.Interfaces;

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
