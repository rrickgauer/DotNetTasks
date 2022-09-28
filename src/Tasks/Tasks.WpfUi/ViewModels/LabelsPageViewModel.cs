using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;
using Tasks.Services.Interfaces;
using Tasks.WpfUi.Services;
using Wpf.Ui.Common.Interfaces;

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

}
