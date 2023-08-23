using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Service.Domain.Models;
using Tasks.Service.Services.Interfaces;
using Tasks.WpfUi.DisplayModels;
using Tasks.WpfUi.Services;
using Tasks.WpfUi.ViewModels.Controls;
using Tasks.WpfUi.Views.Controls;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.ViewModels.Pages;

public partial class ChecklistsViewModel : ObservableObject, INavigationAware
{
    #region - Private Members -
    private readonly IChecklistServices _checklistServices;
    private readonly ChecklistsSidebarViewModel _checklistsSidebarViewModel;
    private readonly CustomAlertServices _customAlertServices;

    private Queue<Guid> OpenChecklistControlIds => new(OpenChecklistControls.Select(c => c.ViewModel.ChecklistId));
    private Queue<Guid> OpenChecklistIdsCache = new();

    #endregion


    #region - Generated Properties -

    /// <summary>
    /// IsProgressRingVisible
    /// </summary>
    [ObservableProperty]
    private bool _isProgressRingVisible = true;

    /// <summary>
    /// OpenChecklistControls
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<OpenChecklistControl> _openChecklistControls = new();



    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="checklistServices"></param>
    public ChecklistsViewModel(IChecklistServices checklistServices, ChecklistsSidebarViewModel checklistsSidebarViewModel, CustomAlertServices customAlertServices)
    {
        _checklistServices = checklistServices;
        _checklistsSidebarViewModel = checklistsSidebarViewModel;
        _customAlertServices = customAlertServices;

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

    private async void OnCloseOpenChecklistEvent(object? sender, Guid checklistId)
    {
        await CloseChecklistAsync(checklistId);
    }

    private void OnDeleteOpenChecklistEvent(object? sender, Guid checklistId)
    {
        int x = 10;
    }

    private void OnOpenChecklistSettingsPageEvent(object? sender, Guid checklistId)
    {
        int x = 10;
    }


    #endregion


    #region - Private Methods -

    private async Task OpenChecklistAsync(Guid checklistId)
    {
        // don't open a checklist that is already open
        if (IsChecklistOpen(checklistId))
        {
            return;
        }

        var control = CreateNewOpenChecklistControl(checklistId);
        OpenChecklistControls.Add(control);

        await control.ViewModel.LoadChecklistData();
    }

    private OpenChecklistControl CreateNewOpenChecklistControl(Guid checklistId)
    {
        OpenChecklistControl control = new(checklistId);
        
        control.ViewModel.CloseOpenChecklistEvent += OnCloseOpenChecklistEvent;
        control.ViewModel.OpenChecklistSettingsPageEvent += OnOpenChecklistSettingsPageEvent;
        control.ViewModel.DeleteOpenChecklistEvent += OnDeleteOpenChecklistEvent;

        return control;
    }

    /// <summary>
    /// Close the specified checklist
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    private async Task CloseChecklistAsync(Guid checklistId)
    {
        // remove the open checklist control
        RemoveOpenChecklistControl(checklistId);

        // close the checklist in the sidebar
        _checklistsSidebarViewModel.CloseChecklist(checklistId);
    }

    /// <summary>
    /// Remove the specified OpenChecklistControl from the page
    /// </summary>
    /// <param name="checklistId"></param>
    private void RemoveOpenChecklistControl(Guid checklistId)
    {
        var control = GetOpenChecklistControl(checklistId);

        // unsubscribe to all its event listeners
        control.ViewModel.CloseOpenChecklistEvent -= OnCloseOpenChecklistEvent;
        control.ViewModel.OpenChecklistSettingsPageEvent -= OnOpenChecklistSettingsPageEvent;
        control.ViewModel.DeleteOpenChecklistEvent -= OnDeleteOpenChecklistEvent;

        // remove it from the list
        OpenChecklistControls.Remove(control);
    }

    /// <summary>
    /// Checks if the specified checklist is open
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    private bool IsChecklistOpen(Guid checklistId)
    {
        return OpenChecklistControlIds.Contains(checklistId);
    }

    /// <summary>
    /// Get the specified open checklist control
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    private OpenChecklistControl GetOpenChecklistControl(Guid checklistId)
    {
        return OpenChecklistControls.Where(c => c.ViewModel.ChecklistId == checklistId).First();
    }

    /// <summary>
    /// Load the page data
    /// </summary>
    /// <returns></returns>
    private async Task LoadDataAsync()
    {
        IsProgressRingVisible = true;

        try
        {
            await _checklistsSidebarViewModel.LoadChecklistsAsync();
            await LoadOpenChecklistsAsync();
            InitOpenChecklistsInSidebar();
        }
        catch(Exception ex)
        {
            _customAlertServices.Error("Could not fetch checklists page data");

#if DEBUG
            throw;
#endif

        }
        finally
        {
            OpenChecklistIdsCache.Clear();
            IsProgressRingVisible = false;
        }

    }

    private async Task LoadOpenChecklistsAsync()
    {
        await Task.WhenAll(OpenChecklistIdsCache.Select(id => OpenChecklistAsync(id)));
    }

    private void InitOpenChecklistsInSidebar()
    {
        foreach (var openChecklistId in OpenChecklistIdsCache)
        {
            _checklistsSidebarViewModel.OpenChecklist(openChecklistId);
        }
    }


    #endregion



    



    #region - INavigationAware -

    public void OnNavigatedFrom()
    {
        foreach (var openChecklistId in OpenChecklistControlIds)
        {
            OpenChecklistIdsCache.Enqueue(openChecklistId);
        }

        OpenChecklistControls.Clear();
    }

    public async void OnNavigatedTo()
    {
        await LoadDataAsync();
    }

    #endregion


}
