﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Services.Interfaces;
using Tasks.WpfUi.Services;
using Tasks.WpfUi.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using Xceed.Wpf.Toolkit;

namespace Tasks.WpfUi.ViewModels;

public partial class EditLabelViewModel : ObservableObject, INavigationAware
{
    #region INavigationAware
    public void OnNavigatedFrom()
    {
        //throw new NotImplementedException();
    }

    public void OnNavigatedTo()
    {
        IsEnabled = true;
    }
    #endregion

    private readonly ILabelServices _labelServices;
    private readonly WpfApplicationServices _applicationServices;
    private readonly INavigation _navigation = App.GetService<INavigationService>().GetNavigationControl();

    public EditLabelViewModel(ILabelServices labelServices, WpfApplicationServices applicationServices)
    {
        _labelServices = labelServices;
        _applicationServices = applicationServices;
    }

    [ObservableProperty]
    private Label? _label;

    [ObservableProperty]
    private bool _isEnabled = true;

    /// <summary>
    /// Go back to the labels page
    /// </summary>
    [RelayCommand]
    public void GoBackToLabelsPage()
    {
        _navigation.Navigate(typeof(LabelsPage));
    }

    /// <summary>
    /// Save the updated labels
    /// </summary>
    [RelayCommand]
    public async void SaveLabelChanges()
    {
        if (AreChangesInvalid())
        {
            return;
        }

        IsEnabled = false;      

        var updateLabelForm = new UpdateLabelForm
        {
            Color = Label.Color,
            Name = Label.Name,
        };

        var response = await _labelServices.UpdateLabelAsync(Label.Id.Value, _applicationServices.User.Id.Value, updateLabelForm);

        IsEnabled = true;

        GoBackToLabelsPage();
    }

    /// <summary>
    /// Make sure the inputs have values
    /// </summary>
    /// <returns></returns>
    private bool AreChangesInvalid()
    {

        if (Label is null)
        {
            return true;
        }
        else if (string.IsNullOrEmpty(Label.Color))
        {
            return true;
        }
        else if (string.IsNullOrEmpty(Label.Name))
        {
            return true;
        }

        return false;
    }

}