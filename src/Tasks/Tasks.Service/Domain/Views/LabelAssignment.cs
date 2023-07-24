using Tasks.Service.Domain.Models;

namespace Tasks.Service.Domain.Views;

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
