using System.Windows.Controls;
using Tasks.WpfUi.ViewModels.Controls;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Controls;

/// <summary>
/// Interaction logic for ChecklistItemsControl.xaml
/// </summary>
public partial class ChecklistItemsControl : UserControl, INavigableView<ChecklistItemsViewModel>
{
    public ChecklistItemsViewModel ViewModel { get; set; }

    public ChecklistItemsControl(ChecklistItemsViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
