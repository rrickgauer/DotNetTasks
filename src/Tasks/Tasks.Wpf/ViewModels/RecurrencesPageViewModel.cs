using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Tasks.CustomAttributes;
using Tasks.Domain.Parms;
using Tasks.Domain.Views;
using Tasks.Services.Interfaces;
using Tasks.Utilities;
using Tasks.Validation;
using Tasks.Wpf.Services;

namespace Tasks.Wpf.ViewModels;

public class RecurrencesPageViewModel : ViewModelBase
{
    public IRecurrenceServices RecurrenceServices { get; set; }
    public WpfApplicationServices ApplicationServices { get; set; }
    public event EventHandler? RecurrencesChanged;
    public event EventHandler? DateChanged;

    public event EventHandler? TestEvent;


    public RecurrencesPageViewModel(IRecurrenceServices recurrenceServices, WpfApplicationServices applicationServices)
    {
        RecurrenceServices = recurrenceServices;
        ApplicationServices = applicationServices;
    }

    #region WeekDate input
    [RaisePropertyChange]
    public DateTime? WeekDate
    {
        get => weekDate;
        set
        {
            if (value == weekDate) return;
            DateChanged?.Invoke(this, new());
            weekDate = value;
            RaisePropertyChanged();
            //SpinnerVisibility = true;

            // refresh the recurrences board
            Task.Run(() => LoadRecurrences(weekDate.Value));
        }
    }
    private DateTime? weekDate = DateTime.Now;

    #endregion

    #region Recurrences data
    [RaisePropertyChange]
    public ObservableCollection<RecurrenceViewModel> Recurrences
    {
        get => _recurrences;
        set
        {
            if (value == _recurrences) return;
            _recurrences = value;
            RaisePropertyChanged();
            RecurrencesChanged?.Invoke(this, new());

            //SpinnerVisibility = false;
        }
    }

    private ObservableCollection<RecurrenceViewModel> _recurrences = new();

    #endregion


    //public Visibility SpinnerVisibility 
    //{ 
    //    get => _spinnerVisibility;
    //    set
    //    {
    //        if (value == _spinnerVisibility) return;
    //        _spinnerVisibility = value;
    //        RaisePropertyChanged();
    //    }
    //}
    //private Visibility _spinnerVisibility = Visibility.Collapsed;


    public bool SpinnerVisibility
    {
        get => _spinnerVisibility;
        set
        {
            //if (value == _spinnerVisibility) return;
            _spinnerVisibility = value;
            RaisePropertyChanged(nameof(SpinnerVisibility));
            TestEvent?.Invoke(this, new());
        }
    }
    private bool _spinnerVisibility = false;


    /// <summary>
    /// Load the recurrrences data
    /// </summary>
    /// <returns></returns>
    public async Task LoadRecurrences()
    {
        await LoadRecurrences(DateTime.Now);
    }

    /// <summary>
    /// Load the recurrrences data
    /// </summary>
    /// <returns></returns>
    public async Task LoadRecurrences(DateTime date)
    {
        var recurrences = await GetReccurences(date);
        Recurrences = new(RecurrenceViewModel.FromCollection(recurrences));
    }

    private async Task<IEnumerable<GetRecurrencesResponse>> GetReccurences(DateTime date)
    {
        ValidDateRange range = date.GetDateRangeFromWeek(DayOfWeek.Monday);

        RecurrenceRetrieval recurrenceRetrieval = new()
        {
            StartsOn = range.StartsOn,
            EndsOn = range.EndsOn,
            UserId = ApplicationServices.User.Id.Value,
        };

        IEnumerable<GetRecurrencesResponse> recurrences = await RecurrenceServices.GetRecurrencesAsync(recurrenceRetrieval);

        return recurrences;
    }


    public void ToggleSpinner(bool showIt)
    {
        SpinnerVisibility = showIt;
    }

}
