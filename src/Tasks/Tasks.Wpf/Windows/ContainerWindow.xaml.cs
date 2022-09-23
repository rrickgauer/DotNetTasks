using System;
using System.Windows;
using Tasks.Wpf.Services;
using Tasks.Wpf.ViewModels;
using Wpf.Ui.Appearance;
using Wpf.Ui.Common;
using Wpf.Ui.Controls.Interfaces;
//using Wpf.Ui.Demo.ViewModels;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.TaskBar;

namespace Tasks.Wpf.Windows;

/// <summary>
/// Interaction logic for Container.xaml
/// </summary>
public partial class ContainerWindow : Window
{
    private readonly WpfApplicationServices _applicationServices;

    public ContainerWindowViewModel ViewModel { get; set; }

    public ContainerWindow(WpfApplicationServices applicationServices, ContainerWindowViewModel viewModel)
    {
        _applicationServices = applicationServices;
        ViewModel = viewModel;

        DataContext = this;

        InitializeComponent();
    }

    private void Window_Closed(object sender, EventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void NavigationItem_Click(object sender, RoutedEventArgs e)
    {

    }
}
