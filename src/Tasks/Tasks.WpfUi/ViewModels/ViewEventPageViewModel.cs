using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Tasks.Domain.Models;
using Tasks.WpfUi.Services;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace Tasks.WpfUi.ViewModels;

public partial class ViewEventPageViewModel : ObservableObject, INavigationAware
{
    private readonly WpfApplicationServices _applicationServices;
    private readonly INavigation _navigation = App.GetService<INavigationService>().GetNavigationControl();

    public ViewEventPageViewModel(WpfApplicationServices applicationServices)
    {
        _applicationServices = applicationServices;
    }

    /// <summary>
    /// The current event
    /// </summary>
    [ObservableProperty]
    private Event? _event;


    private INavigationItem? _previousNavigationItem = null;

    [RelayCommand]
    public async void GoBack()
    {
        if (_previousNavigationItem is null) return;

        _navigation.Navigate(_previousNavigationItem.PageTag);
    }

    #region INavigationAware

    /// <summary>
    /// Record the page from which this page was navigated from
    /// </summary>
    public void OnNavigatedTo()
    {
        _previousNavigationItem = _navigation.Current;
    }

    public void OnNavigatedFrom()
    {
        //throw new System.NotImplementedException();
    }
    #endregion
}
