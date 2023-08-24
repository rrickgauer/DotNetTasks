using System.Windows.Controls;
using Tasks.WpfUi.ViewModels.Pages;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Pages;

/// <summary>
/// Interaction logic for ViewEventPage.xaml
/// </summary>
public partial class ViewEventPage : INavigableView<ViewEventPageViewModel>
{
    public ViewEventPageViewModel ViewModel { get; set; }

    public ViewEventPage(ViewEventPageViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    private void startsOnDatepickerInput_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
        this.endsOnDatepickerInput.DisplayDateStart = this.startsOnDatepickerInput.SelectedDate;
        this.endsOnDatepickerInput.SelectedDate = this.startsOnDatepickerInput.SelectedDate;
    }
}
