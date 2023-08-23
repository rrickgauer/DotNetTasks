using Tasks.WpfUi.ViewModels.Pages;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Pages.Checklists;

/// <summary>
/// Interaction logic for ChecklistsPage.xaml
/// </summary>
public partial class ChecklistsPage : INavigableView<ChecklistsViewModel>
{
    public ChecklistsViewModel ViewModel { get; set; }

    public ChecklistsPage(ChecklistsViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }   
}
