using Tasks.WpfUi.ViewModels.Pages;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Pages
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel
        {
            get;
        }

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();

            ViewModel.InvalidLogin += ViewModel_InvalidLogin;
        }

        private void ViewModel_InvalidLogin(object? sender, System.EventArgs e)
        {
            //this.snackBar.Show();
        }
    }
}