using Tasks.WpfUi.ViewModels;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Pages;

/// <summary>
/// Interaction logic for EditLabelPage.xaml
/// </summary>
public partial class EditLabelPage : INavigableView<EditLabelViewModel>
{
    public EditLabelViewModel ViewModel { get; set; }

    public EditLabelPage(EditLabelViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
