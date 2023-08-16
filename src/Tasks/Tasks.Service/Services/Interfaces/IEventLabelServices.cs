using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Domain.Responses.Custom;

namespace Tasks.Service.Services.Interfaces;

public interface IEventLabelServices
{
    Task<IEnumerable<Label>> GetAssignedLabelsAsync(Guid eventId);
    string GetUri(EventLabel eventLabel);
    Task<int> CreateBatchAsync(EventLabelsBatchRequest eventLabelsBatchRequest);
    Task<IEnumerable<EventLabel>> GetAll(Guid userId);
    Task<IEnumerable<LabelAssignment>> GetLabelAssignmentsForEvent(Guid eventId, Guid userId);
    Task<EventLabel?> SaveAsync(EventLabelForm eventLabelRequestParms);
    Task<bool> DeleteAsync(EventLabelForm eventLabelForm);
}
