using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Parms;
using Tasks.Domain.Views;
using Tasks.Services.Interfaces;
using Tasks.Utilities;
using Tasks.Validation;
using Tasks.WpfUi.Services;
using Tasks.WpfUi.Views.Controls;
using Tasks.WpfUi.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace Tasks.WpfUi.ViewModels;

public partial class RecurrencesPageViewModel : ObservableObject, INavigationAware
{
    private readonly IRecurrenceServices _recurrenceServices;
    private readonly WpfApplicationServices _applicationServices;
    private readonly INavigation _navigation = App.GetService<INavigationService>().GetNavigationControl();
    private readonly ViewEventPage _viewEventPage = App.GetService<IPageService>().GetPage<ViewEventPage>();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="recurrenceServices"></param>
    /// <param name="applicationServices"></param>
    public RecurrencesPageViewModel(IRecurrenceServices recurrenceServices, WpfApplicationServices applicationServices)
    {
        _recurrenceServices = recurrenceServices;
        _applicationServices = applicationServices;
    }

    private bool _initalLoad = false;

    [ObservableProperty]
    private bool _isLoading = true;

    partial void OnIsLoadingChanged(bool value)
    {
        RecurrencesVisible = !value;
    }

    [ObservableProperty]
    private bool _recurrencesVisible = false;

    [ObservableProperty]
    private IEnumerable<DailyRecurrencesControl> _recurrences = new List<DailyRecurrencesControl>();

    partial void OnRecurrencesChanged(IEnumerable<DailyRecurrencesControl> value)
    {
        IsLoading = false;
    }


    [ObservableProperty]
    private DateTime _date = DateTime.Now;

    async partial void OnDateChanged(DateTime value)
    {
        DisplayRecurrences(value);
    }

    [ObservableProperty]
    private DateTime _dateNextWeek = DateTime.Now.AddDays(7);

    [ObservableProperty]
    private DateTime _datePreviousWeek = DateTime.Now.AddDays(-7);

    #region INavigationAware
    public void OnNavigatedFrom() { }
    
    public async void OnNavigatedTo() 
    {
        if (_initalLoad)
        {
            DisplayRecurrences(Date);
        }
    }
    #endregion


    #region Load recurrences

    /// <summary>
    /// Display the recurrences within the week of the specified day
    /// </summary>
    /// <param name="date"></param>
    public async void DisplayRecurrences(DateTime date)
    {
        _initalLoad = true;
        IsLoading = true;

        DateNextWeek = date.AddDays(7);
        DatePreviousWeek = date.AddDays(-7);

        await LoadRecurrences(date);
    }

    /// <summary>
    /// Set the weekly recurrences 
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public async Task LoadRecurrences(DateTime date)
    {
        // get all the user's recurrences for the week
        var recurrencesList = (await GetRecurrencesAsync(date)).Where(r => r.Cancelled == false);

        // get the week's starting/ending date
        ValidDateRange range = date.GetDateRangeFromWeek(DayOfWeek.Monday);

        List<DailyRecurrencesControl> controls = new();
        
        // separate the recurrences collection into each day of the week
        for (DateTime d = range.StartsOn; d != range.EndsOn; d = d.AddDays(1))
        {
            // get the recurrences for the date
            var recurrencesToday = recurrencesList.Where(r => r.OccursOn == d).ToList();

            // setup a new view model for the daily recurrences control
            DailyRecurrencesViewModel viewModel = new(d, recurrencesToday);

            // add a new control to the collection
            controls.Add(new(viewModel));
        }

        // update the UI
        Recurrences = controls;
    }

    /// <summary>
    /// Fetch the user's recurrences that are occur in the week of the given date
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    private async Task<IEnumerable<GetRecurrencesResponse>> GetRecurrencesAsync(DateTime date)
    {
        ValidDateRange range = date.GetDateRangeFromWeek(DayOfWeek.Monday);

        RecurrenceRetrieval recurrenceRetrieval = new()
        {
            StartsOn = range.StartsOn,
            EndsOn = range.EndsOn,
            UserId = _applicationServices.User.Id.Value,
        };

        var recurrences = await _recurrenceServices.GetRecurrencesAsync(recurrenceRetrieval);

        return recurrences;
    }

    #endregion

    /// <summary>
    /// Jump to the specified date
    /// </summary>
    /// <param name="newDate"></param>
    [RelayCommand]
    public void GotoDifferentDay(object newDate)
    {
        Date = (DateTime)newDate;
    }

    /// <summary>
    /// Create a new event and go to the ViewEvent page
    /// </summary>
    [RelayCommand]
    public void CreateNewEvent()
    {
        _viewEventPage.ViewModel.SetupNewEvent(DateTime.Now);

        // navidate to the ViewEvent page
        _navigation.Navigate(_viewEventPage.GetType());
    }

}
