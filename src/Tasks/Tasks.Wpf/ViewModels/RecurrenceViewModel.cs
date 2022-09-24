using System.Collections.Generic;
using System.Linq;
using Tasks.Domain.Views;

namespace Tasks.Wpf.ViewModels;

public class RecurrenceViewModel : ViewModelBase
{
    private GetRecurrencesResponse _getRecurrencesResponse;
    public GetRecurrencesResponse GetRecurrencesResponse 
    { 
        get => _getRecurrencesResponse;
        set
        {
            _getRecurrencesResponse = value;
            RaisePropertyChanged(nameof(GetRecurrencesResponse));
        }    
    }

    public RecurrenceViewModel(GetRecurrencesResponse getRecurrencesResponse)
    {
        _getRecurrencesResponse = getRecurrencesResponse;
    }

    public static IEnumerable<RecurrenceViewModel> FromCollection(IEnumerable<GetRecurrencesResponse> getRecurrencesResponses)
    {
        var objects = 
            from r in getRecurrencesResponses
            select new RecurrenceViewModel(r);

        return objects;
    }

}
