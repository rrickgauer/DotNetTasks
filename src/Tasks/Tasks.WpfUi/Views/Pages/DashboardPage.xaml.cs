using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Pages
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : INavigableView<ViewModels.DashboardViewModel>
    {
        public ViewModels.DashboardViewModel ViewModel
        {
            get;
        }

        public DashboardPage(ViewModels.DashboardViewModel viewModel)
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