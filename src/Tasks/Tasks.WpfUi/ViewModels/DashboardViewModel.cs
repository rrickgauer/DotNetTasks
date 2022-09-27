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

    public event EventHandler? InvalidLogin;

    public DashboardViewModel(WpfApplicationServices userServices)
    {
        ApplicationServices = userServices;
    }

    public void OnNavigatedTo()
    {
    }

    public void OnNavigatedFrom()
    {

    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(InputsHaveValue))]
    private string _email = string.Empty;

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


    [RelayCommand]
    public async void Login()
    {
        var successfulLogin = await ApplicationServices.LogInUser(_email, _password);
            
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
        App.GetService<INavigationService>().Navigate(typeof(RecurrencesPage));
    }
}


