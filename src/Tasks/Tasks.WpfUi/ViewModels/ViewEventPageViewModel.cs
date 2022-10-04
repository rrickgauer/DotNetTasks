using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using Tasks.Domain.Models;
using Tasks.Services.Interfaces;
using Tasks.WpfUi.Services;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace Tasks.WpfUi.ViewModels;

public partial class ViewEventPageViewModel : ObservableObject, INavigationAware
{
    private readonly WpfApplicationServices _applicationServices;
    private readonly IEventServices _eventServices;
    private readonly INavigation _navigation = App.GetService<INavigationService>().GetNavigationControl();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="applicationServices"></param>
    public ViewEventPageViewModel(WpfApplicationServices applicationServices, IEventServices eventServices)
    {
        _applicationServices = applicationServices;
        _eventServices = eventServices;
    }

    /// <summary>
    /// The current event
    /// </summary>
    [ObservableProperty]
    private Event? _event = null;

    [ObservableProperty]
    private bool _formIsEnabled = true;


    /// <summary>
    /// Contains the previous page from which this page was navigated to
    /// </summary>
    private INavigationItem? _previousNavigationItem = null;

    [RelayCommand]
    public void GoBack()
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
        _previousNavigationItem = _navigation.Current;  // Record the page from which this page was navigated from
        FormIsEnabled = true;
    }

    public void OnNavigatedFrom()
    {
        //throw new System.NotImplementedException();
    }
    #endregion


    [RelayCommand]
    public async Task SaveEvent()
    {
        FormIsEnabled = false;

        var updateResult = await _eventServices.UpdateEventAsync(Event);

        if (updateResult is null)
        {
            throw new System.Exception("Error saving the event!");
        }

        FormIsEnabled = true;

        GoBack();
    }
}
