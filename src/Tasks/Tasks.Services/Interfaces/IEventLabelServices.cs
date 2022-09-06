using Tasks.Domain.Models;
using Tasks.Domain.Parms;

namespace Tasks.Services.Interfaces;

public interface IEventLabelServices
{
    Task<EventLabel?> CreateAsync(EventLabelRequestParms eventLabelRequestParms, Guid userId);
    string GetUrl(EventLabel eventLabel);
}
