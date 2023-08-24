using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Service.Services.Interfaces;
using Tasks.WpfUi.Messaging;
using Tasks.WpfUi.Services;
using Tasks.WpfUi.ViewModels.Controls;
using Tasks.WpfUi.Views.Controls;
using Wpf.Ui.Common.Interfaces;
using static Tasks.WpfUi.Messaging.Messages;

namespace Tasks.WpfUi.ViewModels.Pages;

public partial class ChecklistsViewModel : ObservableObject, INavigationAware, ITaskMessenger,
    IRecipient<CloseOpenChecklistMessage>,
    IRecipient<DeleteChecklistMessage>,
    IRecipient<OpenChecklistSettingsPageMessage>,
    IRecipient<OpenChecklistControlMessage>

{
    #region - Private Members -
    private readonly IChecklistServices _checklistServices;
    private readonly ChecklistsSidebarViewModel _checklistsSidebarViewModel;
    private readonly CustomAlertServices _customAlertServices;

    private Queue<Guid> OpenChecklistControlIds => new(OpenChecklistControls.Select(c => c.ViewModel.ChecklistId));
    
    private readonly Queue<Guid> OpenChecklistIdsCache = new();

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

        RegisterMessenger();
    }


    #region - ITaskMessenger Event Handlers -

    public async void Receive(OpenChecklistControlMessage message)
    {
        await OpenChecklistAsync(message.Value);
    }

    
    public void Receive(CloseOpenChecklistMessage message)
    {
        CloseChecklist(message.Value);
    }

    public async void Receive(DeleteChecklistMessage message)
    {
        await DeleteChecklistAsync(message.Value);
    }

    public void Receive(OpenChecklistSettingsPageMessage message)
    {
        // open checklist settings page
        var checklistId = message.Value;
    }

    #endregion


    #region - Private Methods -

    /// <summary>
    /// Delete the specified checklist
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    private async Task DeleteChecklistAsync(Guid checklistId)
    {
        if (MessageBoxServices.Confirm("Are you sure you want to delete this checklist?", "Delete checklist"))
        {
            RemoveOpenChecklistControl(checklistId);

            await _checklistsSidebarViewModel.DeleteChecklistTaskAsync(checklistId);

            _customAlertServices.Successful("Checklist was deleted!");
        }
    }

    /// <summary>
    /// Open the specified checklist
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Create a new OpenChecklistControl using the specified checklist id
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    private static OpenChecklistControl CreateNewOpenChecklistControl(Guid checklistId)
    {
        OpenChecklistControl control = new(checklistId);
        
        return control;
    }

    /// <summary>
    /// Close the specified checklist
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    private void CloseChecklist(Guid checklistId)
    {
        // remove the open checklist control
        RemoveOpenChecklistControl(checklistId);
    }

    /// <summary>
    /// Remove the specified OpenChecklistControl from the page
    /// </summary>
    /// <param name="checklistId"></param>
    private void RemoveOpenChecklistControl(Guid checklistId)
    {
        var control = GetOpenChecklistControl(checklistId);

        // unsubscribe to all its event listeners
        control.ViewModel.CleanUp();

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
    
    /// <summary>
    /// Using the open checklist id cache, open the checklists that were previously opened
    /// </summary>
    /// <returns></returns>
    private async Task LoadOpenChecklistsAsync()
    {
        await Task.WhenAll(OpenChecklistIdsCache.Select(id => OpenChecklistAsync(id)));
    }

    /// <summary>
    /// Set each sidebar checklist item to selected if its ID is cached
    /// </summary>
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


    #region - ITaskMessenger -

    public void RegisterMessenger()
    {
        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    public void CleanUp()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);
        WeakReferenceMessenger.Default.Cleanup();
    }

    #endregion
}
