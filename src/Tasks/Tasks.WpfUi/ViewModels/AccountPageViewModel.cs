using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;
using Tasks.Domain.Views;
using Tasks.Services.Interfaces;
using Tasks.WpfUi.Services;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.ViewModels;

public partial class AccountPageViewModel : ObservableObject, INavigationAware
{
    #region INavigationAware
    public void OnNavigatedFrom()
    {
        //throw new NotImplementedException();
    }

    public async void OnNavigatedTo()
    {
        IsLoading = true;

        await LoadUserData();

        IsLoading = false;
    }
    #endregion

    private readonly WpfApplicationServices _applicationServices;
    private readonly IUserServices _userServices;

    private GetUserResponse _user;

    public AccountPageViewModel(WpfApplicationServices applicationServices, IUserServices userServices)
    {
        _applicationServices = applicationServices;
        _userServices = userServices;

        LoadUserData();
    }


    #region Password values

    [ObservableProperty]
    private string? _currentPassword = null;

    [ObservableProperty]
    private string? _newPassword = null;

    [ObservableProperty]
    private string? _confirmPassword = null;

    #endregion

    [ObservableProperty]
    private bool _isLoading = false;


    [ObservableProperty]
    private bool _isReceiveDailyRemindersChecked;

    partial void OnIsReceiveDailyRemindersCheckedChanged(bool value)
    {
        IsUpdateEmailPreferencesButtonEnabled = true;
    }


    [ObservableProperty]
    private bool _isUpdateEmailPreferencesButtonEnabled = false;



    public async Task LoadUserData()
    {
        _user = await _userServices.GetUserViewAsync(_applicationServices.User.Id.Value) ?? new();

        IsReceiveDailyRemindersChecked = _user.DeliverReminders ?? false;

        IsUpdateEmailPreferencesButtonEnabled = false;
    }

}
