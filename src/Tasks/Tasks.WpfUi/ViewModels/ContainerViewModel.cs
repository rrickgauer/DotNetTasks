
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace Tasks.WpfUi.ViewModels;

public partial class ContainerViewModel : ObservableObject
{
    private bool _isInitialized = false;

    [ObservableProperty]
    private string _applicationTitle = string.Empty;

    [ObservableProperty]
    private ObservableCollection<INavigationControl> _navigationItems = new();

    [ObservableProperty]
    private ObservableCollection<INavigationControl> _navigationFooter = new();

    [ObservableProperty]
    private ObservableCollection<MenuItem> _trayMenuItems = new();

    [ObservableProperty]
    private bool _isLoggedIn = false;

    public ContainerViewModel(INavigationService navigationService)
    {
        if (!_isInitialized)
            InitializeViewModel();
    }

    private void InitializeViewModel()
    {
        ApplicationTitle = "Tasks";

        NavigationItems = new ObservableCollection<INavigationControl>();

        NavigationFooter = new ObservableCollection<INavigationControl>
        {
            new NavigationItem()
            {
                Content = "Settings",
                PageTag = "settings",
                Icon = SymbolRegular.Settings24,
                PageType = typeof(Views.Pages.SettingsPage)
            }
        };

        TrayMenuItems = new ObservableCollection<MenuItem>
        {
            new MenuItem
            {
                Header = "Home",
                Tag = "tray_home"
            }
        };

        _isInitialized = true;
    }


    public void UserLoggedIn()
    {
        NavigationItems = new ObservableCollection<INavigationControl>
        {
            new NavigationItem()
            {
                Content = "Recurrences",
                PageTag = "recurrences",
                Icon = SymbolRegular.CalendarLtr32,
                PageType = typeof(Views.Pages.RecurrencesPage),
                Visibility = System.Windows.Visibility.Visible,
            },
            new NavigationItem()
            {
                Content = "Labels",
                PageTag = "labels",
                Icon = SymbolRegular.Tag32,
                PageType = typeof(Views.Pages.LabelsPage),
                Visibility = System.Windows.Visibility.Visible,
            },

            new NavigationItem()
            {
                Content = "Account",
                PageTag = "account",
                Icon = SymbolRegular.Person24,
                PageType = typeof(Views.Pages.AccountPage),
                Visibility = System.Windows.Visibility.Visible,
            },
        };

        IsLoggedIn = true;
    }

}
