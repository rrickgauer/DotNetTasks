using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Service.Domain.Parms;

public class GetRecurrencesQueryParms
{
    [BindRequired]
    public DateTime StartsOn { get; set; }

    [BindRequired]
    public DateTime EndsOn { get; set; }

    public string? Labels { get; set; }
}
