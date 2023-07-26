using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tasks.Service.Domain.Responses.Custom;

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

    /// <summary>
    /// Pass up the scroll event to the parent control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void recurrencesListView_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
    {
        if (!e.Handled)
        {
            e.Handled = true;
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = UIElement.MouseWheelEvent;
            eventArg.Source = sender;
            var parent = ((Control)sender).Parent as UIElement;
            parent.RaiseEvent(eventArg);
        }
    }

    private void DropdownButton_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        button.ContextMenu.IsOpen = true;

        ViewModel.SelectedRecurrence = button.DataContext as GetRecurrencesResponse;
    }
}
