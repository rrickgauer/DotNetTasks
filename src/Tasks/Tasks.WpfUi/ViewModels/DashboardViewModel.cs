using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using Tasks.Services.Interfaces;
using Tasks.WpfUi.Services;
using Tasks.WpfUi.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace Tasks.WpfUi.ViewModels;

public partial class DashboardViewModel : ObservableObject, INavigationAware, INotifyPropertyChanged
{
    public WpfApplicationServices ApplicationServices { get; set; }
    public INavigationService NavigationService { get; set; }

    public event EventHandler? InvalidLogin;

    public DashboardViewModel(WpfApplicationServices applicationServices, INavigationService navigationService)
    {
        ApplicationServices = applicationServices;
        NavigationService = navigationService;
    }

    #region INavigationAware
    public void OnNavigatedTo() {}
    public void OnNavigatedFrom() {}
    #endregion


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(InputsHaveValue))]
    private string _email = "rrickgauer1@gmail.com";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(InputsHaveValue))]
    private string _password = string.Empty;
    
    public bool InputsHaveValue => DoInputsHaveValue();
    private bool DoInputsHaveValue()
    {
        if (string.IsNullOrEmpty(_email)) return false;
        if (string.IsNullOrEmpty(_password)) return false;
        return true;
    }

    
    [ObservableProperty]
    private bool _isLoading = true;


    [RelayCommand]
    public async void Login()
    {
        var successfulLogin = await ApplicationServices.LogInUser(Email, Password);
            
        if (!successfulLogin)
        {
            InvalidLogin?.Invoke(this, new());
            Email = string.Empty;
            Password = string.Empty;
            return;
        }

        // show the sidebar 
        var containerVM = App.GetService<ContainerViewModel>();
        containerVM.UserLoggedIn();

        // go to the reccurences page
        NavigationService.Navigate(typeof(RecurrencesPage));
    }
}


