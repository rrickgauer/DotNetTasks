using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using Tasks.Service.Domain.Models;
using Tasks.Service.Services.Interfaces;
using Wpf.Ui.Common.Interfaces;
using Tasks.WpfUi.Helpers;
using Wpf.Ui.Mvvm.Contracts;
using Tasks.WpfUi.Views.Pages;
using Wpf.Ui.Controls.Interfaces;
using System.Windows;
using Tasks.Service.Services.Implementations;

namespace Tasks.WpfUi.ViewModels.Pages;

public partial class LabelsPageViewModel : ObservableObject, INavigationAware
{
    #region INavigationAware
    public void OnNavigatedFrom()
    {
        //throw new NotImplementedException();
    }

    public async void OnNavigatedTo()
    {
        await LoadLabelsAsync();
    }
    #endregion

    private readonly ILabelServices _labelServices;
    private readonly WpfApplicationServices _applicationServices;
    private readonly INavigation _navigation = App.GetService<INavigationService>().GetNavigationControl();
    private readonly EditLabelPage _editLabelPage = App.GetService<IPageService>().GetPage<EditLabelPage>();

    public LabelsPageViewModel(ILabelServices labelServices, WpfApplicationServices applicationServices)
    {
        _labelServices = labelServices;
        _applicationServices = applicationServices;
    }

    [ObservableProperty]
    private bool _showProgress = true;

    [ObservableProperty]
    private bool _showLabels = false;

    [ObservableProperty]
    private List<Label> _labels = new();

    [ObservableProperty]
    private Label? _selectedLabel = null;


    #region New Label Form

    [ObservableProperty]
    private string? _newLabelName;

    [ObservableProperty]
    private Color? _newLabelColor;

    [RelayCommand]
    public async Task CreateLabelFromForm()
    {
        var name = NewLabelName;
        var color = NewLabelColor;

        if (string.IsNullOrEmpty(name))
            return;
        else if (color is null)
            return;

        await CreateNewLabel(name, color.Value);

        await LoadLabelsAsync();

        ShowNewLabelForm(false);
        NewLabelColor = null;
        NewLabelName = null;
    }

    /// <summary>
    /// Create the new label.
    /// Returns whether or not it was successfully created.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public async Task<bool> CreateNewLabel(string name, Color color)
    {
        Label newLabel = new()
        {
            Id = Guid.NewGuid(),
            Name = name,
            UserId = _applicationServices.CurrentUserId,
            Color = color.ToHexString(),
            CreatedOn = DateTime.Now,
        };

        var result = await _labelServices.SaveLabelAsync(newLabel);

        return result != null;
    }


    [ObservableProperty]
    private bool _newLabelFormVisible = false;

    /// <summary>
    /// Change the NewLabelForm's visibility.
    /// True - show the form
    /// False - hide the form
    /// </summary>
    /// <param name="isVisible"></param>
    [RelayCommand]
    public void ShowNewLabelForm(bool isVisible)
    {
        NewLabelFormVisible = isVisible;
    }

    /// <summary>
    /// Toggle the New Label Form visibility
    /// Close it if it's open, or vice versa.
    /// </summary>
    [RelayCommand]
    public void ToggleNewLabelForm()
    {
        NewLabelFormVisible = !NewLabelFormVisible;
    }

    #endregion

    /// <summary>
    /// Load the labels async
    /// </summary>
    public async Task LoadLabelsAsync()
    {
        ShowProgress = true;
        ShowLabels = false;

        var labels = await _labelServices.GetLabelsAsync(_applicationServices.CurrentUserId);
        labels ??= new List<Label>();
        Labels = labels.ToList();

        ShowProgress = false;
        ShowLabels = true;
    }

    /// <summary>
    /// Navigate to the edit label page
    /// </summary>
    [RelayCommand]
    public void EditCurrentLabel()
    {
        _editLabelPage.ViewModel.Label = SelectedLabel;
        _navigation.Navigate(_editLabelPage.GetType());
    }

    /// <summary>
    /// Delete the currently selected label
    /// </summary>
    [RelayCommand]
    public async Task DeleteCurrentLabel()
    {
        MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this label?", "Confirmation",  MessageBoxButton.YesNo);

        if (result != MessageBoxResult.Yes)
        {
            return;
        }

        ShowProgress = true;
        ShowLabels = false;

        await _labelServices.DeleteLabelAsync(SelectedLabel.Id.Value);

        await LoadLabelsAsync();
    }

}
