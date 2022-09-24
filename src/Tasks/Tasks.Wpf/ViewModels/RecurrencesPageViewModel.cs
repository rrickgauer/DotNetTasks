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
using Tasks.Services.Interfaces;
using Tasks.Utilities;
using Tasks.Validation;
using Tasks.Wpf.Services;

namespace Tasks.Wpf.ViewModels;

public class RecurrencesPageViewModel : ViewModelBase
{
    public IRecurrenceServices RecurrenceServices { get; set; }
    public WpfApplicationServices ApplicationServices { get; set; }

    public event EventHandler RecurrencesChanged;

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
            weekDate = value;
            RaisePropertyChanged();
            Task.Run(() => LoadRecurrences(weekDate.Value));
        }
    }
    private DateTime? weekDate = DateTime.Now;

    #endregion

    #region Recurrences data
    public ObservableCollection<RecurrenceViewModel> Recurrences
    {
        get => _recurrences;
        set
        {
            if (value == _recurrences) return;
            _recurrences = value;
            RaisePropertyChanged();
            RecurrencesChanged?.Invoke(this, new());
        }
    }

    private ObservableCollection<RecurrenceViewModel> _recurrences = new();


    #endregion


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
        ValidDateRange range = date.GetDateRangeFromWeek(DayOfWeek.Monday);

        RecurrenceRetrieval recurrenceRetrieval = new()
        {
            StartsOn = range.StartsOn,
            EndsOn = range.EndsOn,
            UserId = ApplicationServices.User.Id.Value,
        };

        var recurrences = await RecurrenceServices.GetRecurrencesAsync(recurrenceRetrieval);
        //Recurrences = null;
        Recurrences = new(RecurrenceViewModel.FromCollection(recurrences));
    }

}
