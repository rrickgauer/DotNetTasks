using Tasks.Domain.Models;
using Tasks.Domain.Parms;

namespace Tasks.Services.Interfaces;

public interface IEventLabelServices
{
    Task<EventLabel?> CreateAsync(EventLabelRequestParms eventLabelRequestParms, Guid userId);
    Task<IEnumerable<Label>> GetEventLabelsAsync(Guid eventId, Guid userId);
    string GetUri(EventLabel eventLabel);
    Task<int> CreateBatchAsync(EventLabelsBatchRequest eventLabelsBatchRequest);



    Task<IEnumerable<EventLabel>> GetUserEventLabelsAsync(Guid userId);
}
