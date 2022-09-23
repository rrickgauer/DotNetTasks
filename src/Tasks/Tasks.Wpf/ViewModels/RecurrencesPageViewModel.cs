using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tasks.CustomAttributes;
using Tasks.Domain.Parms;
using Tasks.Domain.Views;
using Tasks.Services.Interfaces;
using Tasks.Wpf.Services;

namespace Tasks.Wpf.ViewModels;

public class RecurrencesPageViewModel : ViewModelBase
{

    public IRecurrenceServices RecurrenceServices { get; set; }
    public WpfApplicationServices ApplicationServices { get; set; }

    public RecurrencesPageViewModel(IRecurrenceServices recurrenceServices, WpfApplicationServices applicationServices)
    {
        RecurrenceServices = recurrenceServices;
        ApplicationServices = applicationServices;

        //LoadRecurrences();
    }

    [RaisePropertyChange]
    public DateTime? WeekDate
    {
        get => weekDate;
        set
        {
            SetPropertyChangedValue(value, ref weekDate);
        }
    }

    private DateTime? weekDate = DateTime.Now;




    [RaisePropertyChange]
    public List<GetRecurrencesResponse> Recurrences 
    { 
        get => _recurrences; 
        set
        {
            SetPropertyChangedValue(value, ref _recurrences);
        }
    }

    private List<GetRecurrencesResponse> _recurrences = new();



    public async Task LoadRecurrences()
    {
        RecurrenceRetrieval recurrenceRetrieval = new()
        { 
            StartsOn = DateTime.Parse("2022-09-18"),
            EndsOn = DateTime.Parse("2022-09-25"),
            UserId = ApplicationServices.User.Id.Value,
        };

        Recurrences = (await RecurrenceServices.GetRecurrencesAsync(recurrenceRetrieval)).ToList() ?? new();
    }

}
