using System.Windows.Controls;
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
