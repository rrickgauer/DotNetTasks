using CommunityToolkit.Mvvm.ComponentModel;
using Tasks.Service.Domain.Models;

namespace Tasks.WpfUi.DisplayModels;

public partial class ChecklistLabelDisplayModel : ObservableObject, IDisplayModel<Label>
{
    public Label Model { get; set; }

    public ChecklistLabelDisplayModel(Label model)
    {
        Model = model;
    }

    [ObservableProperty]
    private bool _isAssigned = false;
}
