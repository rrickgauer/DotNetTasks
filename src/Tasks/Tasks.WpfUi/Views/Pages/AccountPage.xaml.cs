using Tasks.WpfUi.ViewModels;
using Tasks.WpfUi.ViewModels.Pages;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Pages;

/// <summary>
/// Interaction logic for AccountPage.xaml
/// </summary>
public partial class AccountPage : INavigableView<AccountPageViewModel>
{
    public AccountPageViewModel ViewModel { get; set; }

    public AccountPage(AccountPageViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
