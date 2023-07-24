using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Views;
using Tasks.Service.Services.Interfaces;
using Tasks.WpfUi.Services;
using Tasks.WpfUi.Views.Pages;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace Tasks.WpfUi.ViewModels;

#pragma warning disable CS8601 // Possible null reference assignment.

public partial class DailyRecurrencesViewModel : ObservableObject
{
    #region Private members
    private readonly IEventActionServices _eventActionServices        = App.GetService<IEventActionServices>();
    private readonly INavigation _navigation                          = App.GetService<INavigationService>().GetNavigationControl();
    private readonly ViewEventPage _viewEventPage                     = App.GetService<IPageService>().GetPage<ViewEventPage>();
    private readonly AssignedEventLabelsPage _assignedEventLabelsPage = App.GetService<IPageService>().GetPage<AssignedEventLabelsPage>();
    private readonly WpfApplicationServices _applicationServices      = App.GetService<WpfApplicationServices>();
    private readonly IEventServices _eventServices                    = App.GetService<IEventServices>();
    #endregion

    [ObservableProperty]
    private DateTime _date = DateTime.Today;

    [ObservableProperty]
    private IEnumerable<GetRecurrencesResponse> _recurrences = new List<GetRecurrencesResponse>();

    [ObservableProperty]
    private GetRecurrencesResponse? _selectedRecurrence;

    [ObservableProperty]
    private bool _isCurrentDate = false;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanCreateNewEvent))]
    private string _newEventName = string.Empty;

    public bool CanCreateNewEvent => !string.IsNullOrEmpty(NewEventName);

    [ObservableProperty]
    private bool _isNewEventFormEnabled = true;


    /// <summary>
    /// Empty constructor
    /// </summary>
    public DailyRecurrencesViewModel() 
    {

    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="date"></param>
    /// <param name="recurrences"></param>
    public DailyRecurrencesViewModel(DateTime date, IEnumerable<GetRecurrencesResponse> recurrences)
    {
        Date = date;
        Recurrences = recurrences;
    }

    [RelayCommand]
    public async Task MarkComplete(GetRecurrencesResponse? recurrenceResponse)
    {
        if (recurrenceResponse is null) return;

        if (recurrenceResponse.Completed.HasValue && recurrenceResponse.Completed.Value == true)
        {
            // save the completion
            await _eventActionServices.SaveEventCompletionAsync(recurrenceResponse.Event.Id.Value, recurrenceResponse.OccursOn.Value);
        }
        else
        {
            // remove the completion
            await _eventActionServices.DeleteEventCompletionAsync(recurrenceResponse.Event.Id.Value, recurrenceResponse.OccursOn.Value);
        }
    }

    [RelayCommand]
    public void ViewEvent(Event event_)
    {
        _viewEventPage.ViewModel.ViewEvent(event_);
        _navigation.Navigate(_viewEventPage.GetType());
    }


    [RelayCommand]
    public async Task ViewAssignedLabels()
    {
        await _assignedEventLabelsPage.ViewModel.ViewAssignedEventLabels(SelectedRecurrence.Event);
        _navigation.Navigate(_assignedEventLabelsPage.GetType());
    }

    [RelayCommand]
    public async Task CancelRecurrence()
    {
        GetRecurrencesResponse? recurrenceToDelete = SelectedRecurrence;

        if (!ConfirmCancellation())
        {
            return;
        }

        // set the recurrence cancelled property to true to update the gui
        var recurrencesList = Recurrences.ToList();
        var index = recurrencesList.IndexOf(recurrenceToDelete);
        recurrencesList.RemoveAt(index);
        Recurrences = recurrencesList;

        await _eventActionServices.SaveEventCancellationAsync(recurrenceToDelete.Event.Id.Value, recurrenceToDelete.OccursOn.Value);
    }


    /// <summary>
    /// Have the user confirm that they want to delete the recurrence.
    /// </summary>
    /// <returns></returns>
    private bool ConfirmCancellation()
    {
        MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel this event?", "Confirmation", MessageBoxButton.YesNo);

        if (result != MessageBoxResult.Yes)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Create a new event for the date
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    public async Task CreateNewEventAsync()
    {
        if (!CanCreateNewEvent) return;

        IsNewEventFormEnabled = false;

        try 
        {
            // create a new event and save it to the database
            Event newEvent = Event.CreateSingleEvent(NewEventName, Date, _applicationServices.CurrentUserId);
            var result = await _eventServices.UpdateEventAsync(newEvent);

            // add a new recurrence item to the collection to update the GUI
            GetRecurrencesResponse newRecurrence = new()
            {
                Event = newEvent,
                Cancelled = false,
                Completed = false,
                OccursOn = newEvent.StartsOn
            };

            var recurrences = Recurrences.ToList();
            recurrences.Add(newRecurrence);
            Recurrences = recurrences.OrderBy(r => r.Event.StartsOn);

            // reset the form 
            NewEventName = string.Empty;
        }
        catch(Exception ex)
        {
            MessageBoxServices.ShowException(ex);
        }
        finally
        {
            IsNewEventFormEnabled = true;
        }
    }





}
