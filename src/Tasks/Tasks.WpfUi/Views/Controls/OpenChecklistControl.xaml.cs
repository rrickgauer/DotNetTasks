using System;
using System.Windows;
using System.Windows.Controls;
using Tasks.WpfUi.ViewModels.Controls;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Controls;

/// <summary>
/// Interaction logic for OpenChecklistControl.xaml
/// </summary>
public partial class OpenChecklistControl : UserControl, INavigableView<OpenChecklistViewModel>
{
    public OpenChecklistViewModel ViewModel { get; private set; }

    public OpenChecklistControl(Guid checklistId)
    {
        ViewModel = new(checklistId);
        DataContext = this;

        InitializeComponent();
    }

    private void OpenChecklistDropdownMenuButton_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        button.ContextMenu.IsOpen = true;
    }
}
