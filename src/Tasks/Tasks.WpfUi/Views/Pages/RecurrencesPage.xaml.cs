using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tasks.WpfUi.ViewModels;
using Tasks.WpfUi.ViewModels.Pages;
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

    /// <summary>
    /// Pass up the mouse scroll event to the parent
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ListView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
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
}
