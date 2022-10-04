using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Views;
using Tasks.Services.Interfaces;
using Tasks.WpfUi.Views.Pages;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

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
    private readonly INavigation _navigation = App.GetService<INavigationService>().GetNavigationControl();
    private readonly ViewEventPage _viewEventPage = App.GetService<IPageService>().GetPage<ViewEventPage>();

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
            _eventActionServices.SaveEventCompletionAsync(recurrenceResponse.Event.Id.Value, recurrenceResponse.OccursOn.Value);
        }
        else
        {
            // remove the completion
            _eventActionServices.DeleteEventCompletionAsync(recurrenceResponse.Event.Id.Value, recurrenceResponse.OccursOn.Value);
        }
    }

    [RelayCommand]
    public void ViewEvent(object sender)
    {
        _navigation.Navigate(_viewEventPage.GetType());
    }


}
