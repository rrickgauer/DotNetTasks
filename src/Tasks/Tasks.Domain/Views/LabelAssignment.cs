using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;

namespace Tasks.Domain.Views;

public class LabelAssignment
{
    public Label Label { get; set; }
    public bool IsAssigned { get; set; } = false;

    public LabelAssignment(Label label, bool isAssigned)
    {
        Label = label;
        IsAssigned = isAssigned;
    }

    public LabelAssignment(Label label)
    {
        Label = label;
    }
}
