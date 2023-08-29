using System.Windows.Controls;
using Tasks.WpfUi.ViewModels.Controls;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Controls;

/// <summary>
/// Interaction logic for ChecklistItemControl.xaml
/// </summary>
public partial class ChecklistItemControl : UserControl, INavigableView<ChecklistItemViewModel>
{
    public ChecklistItemViewModel ViewModel { get; set; }

    public ChecklistItemControl(ChecklistItemViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
        
    }
}
