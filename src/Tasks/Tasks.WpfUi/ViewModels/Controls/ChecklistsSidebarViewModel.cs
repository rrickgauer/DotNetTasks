using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Services.Implementations;
using Tasks.Service.Services.Interfaces;
using Tasks.WpfUi.DisplayModels;
using Tasks.WpfUi.Messaging;
using Tasks.WpfUi.Services;
using static Tasks.WpfUi.Messaging.Messages;

namespace Tasks.WpfUi.ViewModels.Controls;

public partial class ChecklistsSidebarViewModel : ObservableObject, ITaskMessenger, IRecipient<CloseOpenChecklistMessage>
{
    #region - Private Members -
    private readonly IChecklistServices _checklistServices;
    private readonly WpfApplicationServices _wpfApplicationServices;
    private readonly CustomAlertServices _customAlertServices;

    private Guid CurrentUserId => _wpfApplicationServices.CurrentUserId;
    #endregion



    #region - Generated Properties -

    /// <summary>
    /// Checklists
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<ChecklistSidebarDisplayModel> _checklists = new();

    /// <summary>
    /// NewChecklistInputText
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CreateNewChecklistCommand))]
    private string _newChecklistInputText = string.Empty;


    /// <summary>
    /// IsNewChecklistFormVisibile
    /// </summary>
    [ObservableProperty]
    private bool _isNewChecklistFormVisible = false;

    /// <summary>
    /// IsLoadingSpinnerVisible
    /// </summary>
    [ObservableProperty]
    private bool _isLoadingSpinnerVisible = false;


    #endregion 


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="checklistServices"></param>
    /// <param name="wpfApplicationServices"></param>
    public ChecklistsSidebarViewModel(IChecklistServices checklistServices, WpfApplicationServices wpfApplicationServices, CustomAlertServices customAlertServices)
    {
        _checklistServices = checklistServices;
        _wpfApplicationServices = wpfApplicationServices;
        _customAlertServices = customAlertServices;

        RegisterMessenger();
    }


    #region - Commands -

    /// <summary>
    /// CreateNewChecklistCommand
    /// </summary>
    /// <returns></returns>
    [RelayCommand(CanExecute = nameof(CanCreateNewChecklistAsync))] 
    private async Task CreateNewChecklistAsync()
    {
        await CreateChecklistAsync(NewChecklistInputText);
    }


    /// <summary>
    /// ToggleNewChecklistVisibilityCommand
    /// </summary>
    [RelayCommand]
    private void ToggleNewChecklistVisibility()
    {
        IsNewChecklistFormVisible = !IsNewChecklistFormVisible;
    }

    /// <summary>
    /// CancelNewChecklistFormCommand
    /// </summary>
    [RelayCommand]
    private void CancelNewChecklistForm()
    {
        IsNewChecklistFormVisible = false;
        NewChecklistInputText = string.Empty;
    }

    #endregion


    #region - CanExecute -

    /// <summary>
    /// Returns true if the NewChecklistInputText has non empty value.
    /// </summary>
    /// <returns></returns>
    private bool CanCreateNewChecklistAsync()
    {
        if (string.IsNullOrWhiteSpace(NewChecklistInputText))
        {
            return false;
        }

        return true;
    }


    #endregion


    #region - Public Methods -

    public void CloseChecklist(Guid checklistId)
    {
        SetChecklistIsSelected(checklistId, false);
    }

    public void OpenChecklist(Guid checklistId)
    {
        SetChecklistIsSelected(checklistId, true);
    }

    public async Task<bool> DeleteChecklistTaskAsync(Guid checklistId)
    {
        var checklist = GetChecklistById(checklistId);

        checklist.CleanUp();

        if (!Checklists.Remove(checklist))
        {
            return false;
        }

        await _checklistServices.DeleteChecklistAsync(checklistId);

        return true;
    }


    #endregion


    public void Receive(CloseOpenChecklistMessage message)
    {
        CloseChecklist(message.Value);
    }

    #region - Private Methods -

    /// <summary>
    /// Load the checklists data
    /// </summary>
    public async Task LoadChecklistsAsync()
    {
        Checklists = await GetChecklistsAsync();

        foreach (var checklist in Checklists)
        {
            checklist.RegisterMessenger();
        }
        
    }

    /// <summary>
    /// Get the checklists from the service
    /// </summary>
    /// <returns></returns>
    private async Task<ObservableCollection<ChecklistSidebarDisplayModel>> GetChecklistsAsync()
    {
        var checklists = await _checklistServices.GetUserChecklistsAsync(_wpfApplicationServices.CurrentUserId);

        var displayModels = checklists.Select(m => new ChecklistSidebarDisplayModel(m));

        return new(displayModels);        
    }

    /// <summary>
    /// Create a new checklist using the specified title
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    private async Task CreateChecklistAsync(string title)
    {
        // save the new checklist to the database
        var newChecklistView = await SaveNewChecklistView(title);
        
        // add it to the controls
        ChecklistSidebarDisplayModel displayModel = new(newChecklistView);
        Checklists = new(Checklists.Prepend(displayModel));

        // clear out the input text
        NewChecklistInputText = string.Empty;

        // show successful alert
        _customAlertServices.Successful("Checklist created!");
    }


    /// <summary>
    /// Create a new checklist and save it to the database using the specified title
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    private async Task<ChecklistView> SaveNewChecklistView(string title)
    {
        Checklist newChecklist = new()
        {
            Id = Guid.NewGuid(),
            ListType = ChecklistType.List,
            Title = title,
            CreatedOn = DateTime.Now,
            UserId = CurrentUserId,
        };


        var checklistView = await _checklistServices.SaveChecklistAsync(newChecklist);

        return checklistView;
    }

    private void SetChecklistIsSelected(Guid checklistId, bool isSelected)
    {
        var checklist = GetChecklistById(checklistId);
        checklist.SetIsSelectedQuietly(isSelected);
    }

    private ChecklistSidebarDisplayModel GetChecklistById(Guid checklistId)
    {
        return Checklists.Where(c => c.Model.Id == checklistId).First();
    }

    #endregion


    #region - ss

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
