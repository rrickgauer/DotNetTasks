using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tasks.WpfUi.ViewModels;
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
