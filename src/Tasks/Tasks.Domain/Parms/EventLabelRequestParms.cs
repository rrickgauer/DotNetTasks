using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Domain.Parms;

public class EventLabelRequestParms
{
    [BindRequired]
    public Guid EventId { get; set; }

    [BindRequired]
    public Guid LabelId { get; set; }

}
