using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Pages
{
    /// <summary>
    /// Interaction logic for DataView.xaml
    /// </summary>
    public partial class DataPage : INavigableView<ViewModels.RecurrencesPageViewModel>
    {
        public ViewModels.RecurrencesPageViewModel ViewModel
        {
            get;
        }

        public DataPage(ViewModels.RecurrencesPageViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();
        }
    }
}
