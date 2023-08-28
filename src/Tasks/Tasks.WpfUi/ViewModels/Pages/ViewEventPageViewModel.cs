using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Utilities;
using Tasks.WpfUi.Services;
using Tasks.WpfUi.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace Tasks.WpfUi.ViewModels.Pages;

public partial class ViewEventPageViewModel : ObservableObject, INavigationAware
{
    private readonly WpfApplicationServices _applicationServices;
    private readonly IEventServices _eventServices;
    private readonly INavigation _navigation = App.GetService<INavigationService>().GetNavigationControl();
    private readonly AssignedEventLabelsPage _assignedEventLabelsPage = App.GetService<IPageService>().GetPage<AssignedEventLabelsPage>();

    private const string DeleteEventConfirmationMessage = "Are you sure you want to delete this event? It cannot be undone.";

    private class SaveButtonTextValues
    {
        public const string ExistingEvent = "Save changes";
        public const string NewEvent = "Create event";
    }

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


    partial void OnEventChanging(Event? value)
    {
        int x = 10;
    }

    [ObservableProperty]
    private bool _formIsEnabled = true;

    [ObservableProperty]
    private IEnumerable<Frequency> _frequencyOptions = EnumUtilities.GetEnumEntries<Frequency>();

    [ObservableProperty]
    private bool _eventExists = false;

    partial void OnEventExistsChanged(bool value)
    {
        SaveButtonText = EventExists ? SaveButtonTextValues.ExistingEvent : SaveButtonTextValues.NewEvent;
    }

    [ObservableProperty]
    private string _saveButtonText = SaveButtonTextValues.NewEvent;


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



    #region Save event

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

    #endregion

    /// <summary>
    /// Clear the form and set up a new event
    /// </summary>
    /// <param name="date"></param>
    public void SetupNewEvent(DateTime date)
    {
        Event newEvent = new()
        {
            Id = Guid.NewGuid(),
            UserId = _applicationServices.CurrentUserId,
            StartsOn = date,
            EndsOn = date,
            Frequency = Frequency.ONCE,
        };

        Event = newEvent;

        EventExists = false;
    }

    /// <summary>
    /// Load the form values with the specied event.
    /// </summary>
    /// <param name="e"></param>
    public void ViewEvent(Event e)
    {
        Event = e;
        EventExists = true;
    }

    /// <summary>
    /// Delete the event
    /// </summary>
    [RelayCommand]
    public async Task DeleteEvent()
    {
        if (!ConfirmDeletion())
        {
            return;
        }

        // disable the form
        FormIsEnabled = false;

        await _eventServices.DeleteEventAsync(Event.Id.Value);

        // enable the form
        FormIsEnabled = true;

        // go back to the previous page
        GoBack();
    }

    /// <summary>
    /// Have the user confirm that they want to delete the event.
    /// </summary>
    /// <returns></returns>
    private bool ConfirmDeletion()
    {
        MessageBoxResult result = MessageBox.Show(DeleteEventConfirmationMessage, "Confirmation", MessageBoxButton.YesNo);

        if (result != MessageBoxResult.Yes)
        {
            return false;
        }

        return true;
    }


    [RelayCommand]
    public async Task GoToLabelAssignmentsPage()
    {
        await _assignedEventLabelsPage.ViewModel.ViewAssignedEventLabels(Event);
        _navigation.Navigate(_assignedEventLabelsPage.GetType());
    }

}
