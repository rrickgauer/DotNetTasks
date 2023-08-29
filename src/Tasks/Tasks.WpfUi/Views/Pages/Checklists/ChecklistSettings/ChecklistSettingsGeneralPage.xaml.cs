using Tasks.WpfUi.ViewModels.Pages.ChecklistSettings;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Pages.Checklists.ChecklistSettings;

/// <summary>
/// Interaction logic for ChecklistSettingsGeneralPage.xaml
/// </summary>
public partial class ChecklistSettingsGeneralPage : IChecklistSettingsPage<ChecklistSettingsGeneralViewModel>
{
    public ChecklistSettingsGeneralViewModel ViewModel { get; set; }

    public ChecklistSettingsGeneralPage(ChecklistSettingsGeneralViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
