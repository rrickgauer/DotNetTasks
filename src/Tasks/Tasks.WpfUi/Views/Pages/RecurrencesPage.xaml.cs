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
/// Interaction logic for RecurrencesPage.xaml
/// </summary>
public partial class RecurrencesPage : INavigableView<RecurrencesPageViewModel>
{
    public RecurrencesPage(RecurrencesPageViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    public RecurrencesPageViewModel ViewModel { get; set; }
}
