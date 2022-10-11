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
        await LoadUserDataAsync();
    }
    #endregion

    private readonly WpfApplicationServices _applicationServices;
    private readonly IUserServices _userServices;
    private GetUserResponse _user;

    public AccountPageViewModel(WpfApplicationServices applicationServices, IUserServices userServices)
    {
        _applicationServices = applicationServices;
        _userServices = userServices;
    }


    #region Password values

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsUpdatePasswordButtonEnabled))]
    private string? _currentPassword = null;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsUpdatePasswordButtonEnabled))]
    private string? _newPassword = null;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsUpdatePasswordButtonEnabled))]
    private string? _confirmPassword = null;

    
    public bool IsUpdatePasswordButtonEnabled => DoPasswordsHaveValues();

    private bool DoPasswordsHaveValues()
    {
        if (string.IsNullOrEmpty(_currentPassword))
            return false;
        if (string.IsNullOrEmpty(_newPassword))
            return false;
        if (string.IsNullOrEmpty(_confirmPassword))
            return false;

        return true;
    }

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

    [ObservableProperty]
    private bool _isSendEmailVerificationButtonEnabled = false;


    /// <summary>
    /// Load the user data
    /// </summary>
    /// <returns></returns>
    public async Task LoadUserDataAsync()
    {
        IsLoading = true;

        _user = await _userServices.GetUserViewAsync(_applicationServices.User.Id.Value) ?? new();

        SetControlsData();

        IsLoading = false;
    }

    /// <summary>
    /// Update the control values to the new user data
    /// </summary>
    private void SetControlsData()
    {
        IsReceiveDailyRemindersChecked = _user.DeliverReminders ?? false;
        IsUpdateEmailPreferencesButtonEnabled = false;

        IsSendEmailVerificationButtonEnabled = !_user.IsConfirmed;
    }

}
