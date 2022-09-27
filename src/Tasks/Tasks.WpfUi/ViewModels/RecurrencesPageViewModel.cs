using CommunityToolkit.Mvvm.ComponentModel;
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
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.ViewModels;

public partial class RecurrencesPageViewModel : ObservableObject, INavigationAware
{
    private readonly IRecurrenceServices _recurrenceServices;
    private readonly WpfApplicationServices _applicationServices;

    public RecurrencesPageViewModel(IRecurrenceServices recurrenceServices, WpfApplicationServices applicationServices)
    {
        _recurrenceServices = recurrenceServices;
        _applicationServices = applicationServices;

        LoadRecurrences(Date);
    }

    [ObservableProperty]
    private string _text = "Recurrences";

    [ObservableProperty]
    private bool _isLoading = true;

    partial void OnIsLoadingChanged(bool value)
    {
        RecurrencesVisible = !value;
    }

    [ObservableProperty]
    private bool _recurrencesVisible = false;


    [ObservableProperty]
    private IEnumerable<GetRecurrencesResponse> _recurrences = new List<GetRecurrencesResponse>();

    partial void OnRecurrencesChanged(IEnumerable<GetRecurrencesResponse> value)
    {
        IsLoading = false;
    }


    [ObservableProperty]
    private DateTime _date = DateTime.Today;

    async partial void OnDateChanged(DateTime value)
    {
        IsLoading = true;
        await LoadRecurrences(value);
    }

    #region INavigationAware
    public void OnNavigatedFrom() { }
    public void OnNavigatedTo() { }
    #endregion

    public async Task LoadRecurrences(DateTime date)
    {
        Recurrences = await GetRecurrencesAsync(date);
    }


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

}
