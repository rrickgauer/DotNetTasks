using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Threading.Tasks;
using Tasks.Service.Services.Interfaces;
using Tasks.WpfUi.DisplayModels;
using Tasks.WpfUi.ViewModels.Controls;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.ViewModels.Pages;

public partial class ChecklistsViewModel : ObservableObject, INavigationAware
{
    #region - Private Members -
    private readonly IChecklistServices _checklistServices;
    private readonly ChecklistsSidebarViewModel _checklistsSidebarViewModel;
    #endregion


    #region - Generated Properties -

    /// <summary>
    /// IsProgressRingVisible
    /// </summary>
    [ObservableProperty]
    private bool _isProgressRingVisible = true;


    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="checklistServices"></param>
    public ChecklistsViewModel(IChecklistServices checklistServices, ChecklistsSidebarViewModel checklistsSidebarViewModel)
    {
        _checklistServices = checklistServices;
        _checklistsSidebarViewModel = checklistsSidebarViewModel;

        _checklistsSidebarViewModel.ChecklistSelectedEvent += OnSidebarChecklistSelectedEvent;
    }


    #region - Event Handlers -

    /// <summary>
    /// Event handler for when a sidebar checklist item is selected
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="checklistId"></param>
    private async void OnSidebarChecklistSelectedEvent(object? sender, ChecklistSidebarDisplayModel checklistSidebarDisplayModel)
    {
        if (checklistSidebarDisplayModel.IsSelected)
        {
            await OpenChecklistAsync(checklistSidebarDisplayModel.Model.Id.Value);
        }
        else
        {
            await CloseChecklistAsync(checklistSidebarDisplayModel.Model.Id.Value);
        }
    }

    #endregion


    #region - Private Methods -

    private async Task OpenChecklistAsync(Guid checklistId)
    {
        int x = 10;
    }

    private async Task CloseChecklistAsync(Guid checklistId)
    {
        int x = 10;
    }


    private async Task LoadDataAsync()
    {
        IsProgressRingVisible = true;
        
        await _checklistsSidebarViewModel.LoadChecklistsAsync();
        // load open checklists

        IsProgressRingVisible = false;

    }

    #endregion

    #region - INavigationAware -

    public void OnNavigatedFrom()
    {
        //throw new NotImplementedException();
    }

    public async void OnNavigatedTo()
    {
        await LoadDataAsync();
    }

    #endregion


}
