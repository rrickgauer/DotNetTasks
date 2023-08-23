using System.Windows.Controls;
using Tasks.WpfUi.ViewModels;
using Tasks.WpfUi.ViewModels.Pages;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Pages;

/// <summary>
/// Interaction logic for AssignedEventLabelsPage.xaml
/// </summary>
public partial class AssignedEventLabelsPage : INavigableView<AssignedEventLabelsViewModel>
{
    public AssignedEventLabelsViewModel ViewModel { get; set; }

    public AssignedEventLabelsPage(AssignedEventLabelsViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
