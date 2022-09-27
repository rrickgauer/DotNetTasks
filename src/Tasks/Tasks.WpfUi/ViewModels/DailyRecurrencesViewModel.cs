using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Views;
using Tasks.Services.Interfaces;

namespace Tasks.WpfUi.ViewModels;

public partial class DailyRecurrencesViewModel : ObservableObject
{
    [ObservableProperty]
    private DateTime _date = DateTime.Today;

    [ObservableProperty]
    private IEnumerable<GetRecurrencesResponse> _recurrences = new List<GetRecurrencesResponse>();

    [ObservableProperty]
    private bool _isExpanded = true;

    [ObservableProperty]
    private GetRecurrencesResponse? _selectedRecurrence;


    private readonly IEventActionServices _eventActionServices = App.GetService<IEventActionServices>();

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
    public void TestMe()
    {
        int x = 10;
    }

    [RelayCommand]
    public async void MarkComplete(GetRecurrencesResponse? recurrenceResponse)
    {
        if (recurrenceResponse is null) return;

        if (recurrenceResponse.Completed.HasValue && recurrenceResponse.Completed.Value == true)
        {
            // save the completion
            //var r = await _eventActionServices.SaveEventCompletionAsync(recurrenceResponse.Event.Id.Value, recurrenceResponse.OccursOn.Value);
            _eventActionServices.SaveEventCompletionAsync(recurrenceResponse.Event.Id.Value, recurrenceResponse.OccursOn.Value);
        }
        else
        {
            // remove the completion
            //var r = await _eventActionServices.DeleteEventCompletionAsync(recurrenceResponse.Event.Id.Value, recurrenceResponse.OccursOn.Value);
            _eventActionServices.DeleteEventCompletionAsync(recurrenceResponse.Event.Id.Value, recurrenceResponse.OccursOn.Value);
        }
    }
}
