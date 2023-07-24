using Tasks.Service.Domain.Models;

namespace Tasks.Service.Domain.Views;

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
