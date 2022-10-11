using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Domain.Views;
using Tasks.Services.Interfaces;
using Tasks.WpfUi.Services;
using Tasks.WpfUi.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace Tasks.WpfUi.ViewModels;

public partial class AssignedEventLabelsViewModel : ObservableObject, INavigationAware
{
    private readonly INavigation _navigation = App.GetService<INavigationService>().GetNavigationControl();
    private readonly LabelsPage _labelsPage = App.GetService<IPageService>().GetPage<LabelsPage>();
    private readonly WpfApplicationServices _applicationServices;
    private readonly IEventLabelServices _eventLabelServices;

    /// <summary>
    /// Flag that sets the spinner's visibility
    /// </summary>
    [ObservableProperty]
    private bool _isLoading = true;

    [ObservableProperty]
    private bool _showLabels = false;


    [ObservableProperty]
    private Event _event = new();

    [ObservableProperty]
    private IEnumerable<LabelAssignment> _labelAssignments = new List<LabelAssignment>();

    /// <summary>
    /// Contains the previous page from which this page was navigated to
    /// </summary>
    private INavigationItem? _previousNavigationItem = null;

    public AssignedEventLabelsViewModel(WpfApplicationServices applicationServices, IEventLabelServices eventLabelServices)
    {
        _applicationServices = applicationServices;
        _eventLabelServices = eventLabelServices;
    }



    #region INavigationAware
    public void OnNavigatedFrom()
    {
        //throw new NotImplementedException();
    }

    public void OnNavigatedTo()
    {
        _previousNavigationItem = _navigation.Current;  // Record the page from which this page was navigated from
        IsLoading = true;
        ShowLabels = false;
    }
    #endregion


    [RelayCommand]
    public void GoBack()
    {
        if (_previousNavigationItem is null) return;

        _navigation.Navigate(_previousNavigationItem.PageTag);
    }


    /// <summary>
    /// View the assigned labels of the specified event
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public async Task ViewAssignedEventLabels(Event e)
    {
        Event = e;

        var labelAssignments = await _eventLabelServices.GetUserEventLabelAssignmentsAsync(Event.Id.Value, _applicationServices.CurrentUserId);
        LabelAssignments = labelAssignments;

        IsLoading = false;
        ShowLabels = true;
    }

    /// <summary>
    /// Toggle the event label assignment
    /// </summary>
    /// <param name="labelAssignment"></param>
    [RelayCommand]
    public async void ToggleLabelAssignment(LabelAssignment labelAssignment)
    {
        // save the assignment
        if (labelAssignment.IsAssigned)
        {
            EventLabelRequestParms parms = new()
            {
                EventId = Event.Id.Value,
                LabelId = labelAssignment.Label.Id.Value,
            };

            await _eventLabelServices.CreateAsync(parms, _applicationServices.CurrentUserId);
        }

        // delete the assignment
        else
        {
            await _eventLabelServices.DeleteAsync(Event.Id.Value, labelAssignment.Label.Id.Value);
        }
    }


    /// <summary>
    /// Navigate to the labels page
    /// </summary>
    [RelayCommand]
    public void GoToLabelsPage()
    {
        _navigation.Navigate(_labelsPage.GetType());
    }

}
