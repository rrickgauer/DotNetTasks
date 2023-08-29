using System.Windows.Controls;
using Tasks.WpfUi.ViewModels.Pages.ChecklistSettings;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Pages.Checklists.ChecklistSettings;

/// <summary>
/// Interaction logic for ChecklistSettingsItemsPage.xaml
/// </summary>
public partial class ChecklistSettingsItemsPage : IChecklistSettingsPage<ChecklistSettingsItemsViewModel>
{
    public ChecklistSettingsItemsViewModel ViewModel { get; set; }

    public ChecklistSettingsItemsPage(ChecklistSettingsItemsViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
