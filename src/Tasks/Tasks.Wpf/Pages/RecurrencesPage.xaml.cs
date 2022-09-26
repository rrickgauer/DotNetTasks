using Microsoft.Extensions.DependencyInjection;
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
using Tasks.Wpf.ViewModels;

namespace Tasks.Wpf.Pages;

/// <summary>
/// Interaction logic for RecurrencesPage.xaml
/// </summary>
public partial class RecurrencesPage : Page, IControlModel<RecurrencesPageViewModel>
{
    public RecurrencesPageViewModel ViewModel { get; set; }

    public RecurrencesPage()
    {
        ViewModel = App.Services.GetRequiredService<RecurrencesPageViewModel>();
        

        InitializeComponent();

        ViewModel.DateChanged += ViewModel_DateChanged;
        ViewModel.RecurrencesChanged += ViewModel_RecurrencesChanged;


        DataContext = this;

        //Task.Run(() => LoadData());
    }

    private void ViewModel_RecurrencesChanged(object? sender, EventArgs e)
    {

        ViewModel.SpinnerVisibility = false;
    }

    private void ViewModel_DateChanged(object? sender, EventArgs e)
    {
        ViewModel.SpinnerVisibility = true;
    }

    private async Task LoadData()
    {
        await ViewModel.LoadRecurrences();
    }
}
