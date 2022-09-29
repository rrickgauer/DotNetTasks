using CommunityToolkit.Mvvm.ComponentModel;
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
using Wpf.Ui.Common.Interfaces;
using Tasks.Utilities;
using Tasks.WpfUi.Helpers;

namespace Tasks.WpfUi.ViewModels;

public partial class LabelsPageViewModel : ObservableObject, INavigationAware
{
    #region INavigationAware
    public void OnNavigatedFrom()
    {
        //throw new NotImplementedException();
    }

    public void OnNavigatedTo()
    {
        LoadLabelsAsync();
    }
    #endregion


    private readonly ILabelServices _labelServices;
    private readonly WpfApplicationServices _applicationServices;

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


    #region New Label Form

    [ObservableProperty]
    private string? _newLabelName;

    [ObservableProperty]
    private Color? _newLabelColor;

    [RelayCommand]
    public async void CreateLabelFromForm()
    {
        var name = NewLabelName;
        var color = NewLabelColor;

        if (string.IsNullOrEmpty(name))
            return;
        else if (color is null)
            return;

        await CreateNewLabel(name, color.Value);

        LoadLabelsAsync();

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
        UpdateLabelForm newLabelForm = new()
        {
            Name = name,
            Color = color.ToHexString(),
        };

        var labelId = Guid.NewGuid();
        var userId = _applicationServices.User.Id.Value;

        var result = await _labelServices.UpdateLabelAsync(labelId, userId, newLabelForm);

        return result.Successful;
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


    public async void LoadLabelsAsync()
    {
        ShowProgress = true;
        ShowLabels = false;

        var labels = (await _labelServices.GetLabelsAsync(_applicationServices.User.Id.Value)).Data;
        labels ??= new List<Label>();
        Labels = labels.ToList();

        ShowProgress = false;
        ShowLabels = true;
    }

    [RelayCommand]
    public void EditLabel(object o)
    {
        int x = 10;
    }

}
