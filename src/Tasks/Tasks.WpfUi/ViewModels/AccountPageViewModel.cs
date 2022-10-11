using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
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

        _user = await _userServices.GetUserViewAsync(_applicationServices.CurrentUserId) ?? new();

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


    #region Account verification

    /// <summary>
    /// Verify the user's account by sending them an email with a verification link in the body
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    public async Task VerifyAccountAsync()
    {
        IsSendEmailVerificationButtonEnabled = false;

        // Create a new account verification record
        UserEmailVerification? verification = await CreateNewVerificationRecord();

        if (verification is null)
        {
            return;
        }

        bool sentSuccessfully = await SendEmailVericationMessage(verification);
        HandleSendEmailVericationMessage(sentSuccessfully);
    }

    /// <summary>
    /// Create a new account verification record
    /// </summary>
    /// <returns></returns>
    private async Task<UserEmailVerification?> CreateNewVerificationRecord()
    {
        UserEmailVerification? verification = await _userEmailVerificationServices.CreateNewAsync(_applicationServices.CurrentUserId);

        if (verification is null)
        {
            MessageBoxServices.ShowMessage("Couldn't create a new UserEmailVerification record");
        }

        return verification;
    }

    /// <summary>
    /// Have the application send an email to the user with the account verification link
    /// </summary>
    /// <param name="verification"></param>
    /// <returns></returns>
    private async Task<bool> SendEmailVericationMessage(UserEmailVerification verification)
    {
        bool sentSuccessfully = await _userEmailVerificationServices.SendEmailAsync(verification);

        return sentSuccessfully;
    }
    
    /// <summary>
    /// Handle the response to the sending of the account email message
    /// </summary>
    /// <param name="success"></param>
    private void HandleSendEmailVericationMessage(bool success)
    {
        if (success)
        {
            MessageBoxServices.ShowMessage("Check your inbox for the verification message which has the next steps.");
        }
        else
        {
            MessageBoxServices.ShowMessage("Could not send the verification message to your inbox!");
        }
    }

    #endregion


    #region Update email preferences
    
    /// <summary>
    /// Update the user's email preferences
    /// </summary>
    [RelayCommand]
    public async void UpdateEmailPreferencesAsync()
    {
        IsUpdateEmailPreferencesButtonEnabled = false;

        UpdateUserRequestForm newUserData = new()
        {
            Email = _user.Email ?? string.Empty,
            DeliverReminders = IsReceiveDailyRemindersChecked,
        };

        var successfulUpdate = await _userServices.UpdateUserAsync(_applicationServices.CurrentUserId, newUserData);

        if (!successfulUpdate)
        {
            MessageBoxServices.ShowMessage("Did not update email preferences!");
        }

        IsUpdateEmailPreferencesButtonEnabled = true;
    }


    #endregion

}
