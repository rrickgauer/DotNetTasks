using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;

namespace Tasks.Domain.Views;

public class LabelFilter
{
    public Label Label { get; set; }
    public bool IsChecked { get; set; } = false;

    public LabelFilter(Label label, bool isChecked)
    {
        Label = label;
        IsChecked = isChecked;
    }

    public LabelFilter(Label label)
    {
        Label = label;
    }
}
