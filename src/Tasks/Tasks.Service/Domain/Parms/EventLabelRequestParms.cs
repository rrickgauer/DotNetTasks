using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Domain.Models;

namespace Tasks.Service.Domain.Parms;

public class EventLabelRequestParms
{
    [BindRequired]
    public Guid EventId { get; set; }

    [BindRequired]
    public Guid LabelId { get; set; }

    public static explicit operator EventLabel(EventLabelRequestParms eventLabelRequest) => new() { EventId=eventLabelRequest.EventId, LabelId=eventLabelRequest.LabelId };

}
