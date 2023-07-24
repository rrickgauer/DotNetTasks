using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Service.Domain.Parms;

public class EventLabelsBatchRequest
{
    public Guid EventId { get; set; }
    public Guid UserId { get; set; }
    public IEnumerable<Guid> LabelIds { get; set; } = Enumerable.Empty<Guid>();

    public EventLabelsBatchRequest(Guid eventId, Guid userId)
    {
        EventId = eventId;
        UserId = userId;
    }

    public EventLabelsBatchRequest(Guid eventId, Guid userId, IEnumerable<Guid> labelIds)
    {
        EventId = eventId;
        UserId = userId;
        LabelIds = labelIds;
    }
}
