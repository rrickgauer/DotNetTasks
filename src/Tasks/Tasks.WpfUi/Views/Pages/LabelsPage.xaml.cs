using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tasks.WpfUi.ViewModels;
using Tasks.WpfUi.ViewModels.Pages;
using Wpf.Ui.Common.Interfaces;
using LabelModel = Tasks.Service.Domain.Models.Label;

namespace Tasks.WpfUi.Views.Pages;

/// <summary>
/// Interaction logic for LabelsPage.xaml
/// </summary>
public partial class LabelsPage : INavigableView<LabelsPageViewModel>
{
    public LabelsPageViewModel ViewModel { get; set; }

    public LabelsPage(LabelsPageViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }

    private void ListView_MouseWheel(object sender, MouseWheelEventArgs e)
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

    private void Card_MouseWheel(object sender, MouseWheelEventArgs e)
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

        ViewModel.SelectedLabel = button.DataContext as LabelModel;
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        int x = 10;
    }
}
