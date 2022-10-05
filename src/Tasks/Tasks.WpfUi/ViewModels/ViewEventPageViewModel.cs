using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Domain.Enums;
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


    [ObservableProperty]
    private IEnumerable<Frequency> _frequencyOptions = Enum.GetValues(typeof(Frequency)).Cast<Frequency>();


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


    /// <summary>
    /// Save the event changes
    /// </summary>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    [RelayCommand]
    public async Task SaveEvent()
    {
        // validate the event changes
        if (IsEventInvalid())
        {
            return;
        }

        // disable the form
        FormIsEnabled = false;

        // send the update request
        var updateResult = await _eventServices.UpdateEventAsync(Event);

        if (updateResult is null)
        {
            throw new System.Exception("Error saving the event!");
        }

        // enable the form
        FormIsEnabled = true;

        // go back to the previous page
        GoBack();
    }


    /// <summary>
    /// Checks if all the required inputs have a value
    /// </summary>
    /// <returns></returns>
    private bool IsEventInvalid()
    {
        if (Event is null)
        {
            return true;
        }
        else if (Event.Id is null)
        {
            return true;
        }
        else if (string.IsNullOrEmpty(Event.Name))
        {
            return true;
        }
        else if (Event.Frequency is null)
        {
            return true;
        }
        else if (Event.StartsOn is null)
        {
            return true;
        }
        else if (Event.EndsOn is null)
        {
            return true;
        }

        return false;
    }


    /// <summary>
    /// Clear the form and set up a new event
    /// </summary>
    /// <param name="date"></param>
    public void SetupNewEvent(DateTime date)
    {
        Event newEvent = new()
        {
            Id = Guid.NewGuid(),
            UserId = _applicationServices.User.Id.Value,
            StartsOn = date,
            EndsOn = date,
            Frequency = Frequency.ONCE,
        };

        Event = newEvent;
    }
}
