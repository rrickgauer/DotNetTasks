using Tasks.WpfUi.ViewModels.Pages.ChecklistSettings;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Pages.Checklists.ChecklistSettings;

/// <summary>
/// Interaction logic for ChecklistSettingsLabelsPage.xaml
/// </summary>
public partial class ChecklistSettingsLabelsPage : IChecklistSettingsPage<ChecklistSettingsLabelsViewModel>
{
    public ChecklistSettingsLabelsViewModel ViewModel { get; set; }

    public ChecklistSettingsLabelsPage(ChecklistSettingsLabelsViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
