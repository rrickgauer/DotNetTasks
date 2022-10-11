using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Tasks.WpfUi.ViewModels;

public partial class AccountPageViewModel : ObservableObject, INavigationAware
{
    #region Injected dependencies
    private readonly WpfApplicationServices _applicationServices;
    private readonly IUserServices _userServices;
    private readonly IUserEmailVerificationServices _userEmailVerificationServices;
    #endregion

    /// <summary>
    /// Constructor with dependencies injected into it
    /// </summary>
    /// <param name="applicationServices"></param>
    /// <param name="userServices"></param>
    /// <param name="userEmailVerificationServices"></param>
    public AccountPageViewModel(WpfApplicationServices applicationServices, IUserServices userServices, IUserEmailVerificationServices userEmailVerificationServices)
    {
        _applicationServices = applicationServices;
        _userServices = userServices;
        _userEmailVerificationServices = userEmailVerificationServices;
    }

    private GetUserResponse _user;


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


    #region INavigationAware
    public void OnNavigatedFrom()
    {
        //throw new NotImplementedException();
    }

    public async void OnNavigatedTo()
    {
        ClearPasswordInputValues();
        await LoadUserDataAsync();
    }
    #endregion


    #region Load user data

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

    #endregion

    /// <summary>
    /// Clear out the values in each of the password fields
    /// </summary>
    private void ClearPasswordInputValues()
    {
        CurrentPassword = string.Empty;
        NewPassword = string.Empty;
        ConfirmPassword= string.Empty;
    }


    [RelayCommand]
    public void VerifyAccount()
    {
        int x = 10;
    }

}
