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

namespace Tasks.WpfUi.Views.Controls;

/// <summary>
/// Interaction logic for DailyRecurrencesControl.xaml
/// </summary>
public partial class DailyRecurrencesControl : UserControl
{
    public DailyRecurrencesViewModel ViewModel { get; set; }

    public DailyRecurrencesControl(DailyRecurrencesViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}
