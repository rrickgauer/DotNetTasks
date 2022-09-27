using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Views;

namespace Tasks.WpfUi.ViewModels;

public partial class DailyRecurrencesViewModel : ObservableObject
{
    [ObservableProperty]
    private DateTime _date = DateTime.Today;

    [ObservableProperty]
    private IEnumerable<GetRecurrencesResponse> _recurrences = new List<GetRecurrencesResponse>();

    [ObservableProperty]
    private bool _isExpanded = true;

    public DailyRecurrencesViewModel(DateTime date, IEnumerable<GetRecurrencesResponse> recurrences)
    {
        Date = date;
        Recurrences = recurrences;
    }
}
