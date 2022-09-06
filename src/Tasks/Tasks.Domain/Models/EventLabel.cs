using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Domain.Models;

public class EventLabel
{
    public Guid? EventId { get; set; }
    public Guid? LabelId { get; set; }
    public DateTime? CreatedOn { get; set; }
}
