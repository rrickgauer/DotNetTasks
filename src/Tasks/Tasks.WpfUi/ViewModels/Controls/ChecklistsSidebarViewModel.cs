using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Services.Interfaces;
using Tasks.WpfUi.DisplayModels;
using Tasks.WpfUi.Services;

namespace Tasks.WpfUi.ViewModels.Controls;

public partial class ChecklistsSidebarViewModel : ObservableObject
{
    #region - Private Members -
    private readonly IChecklistServices _checklistServices;
    private readonly WpfApplicationServices _wpfApplicationServices;
    private readonly CustomAlertServices _customAlertServices;

    private Guid CurrentUserId => _wpfApplicationServices.CurrentUserId;
    #endregion

    #region - Events -
    public event EventHandler<ChecklistSidebarDisplayModel> ChecklistSelectedEvent;
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

        checklist.IsSelectedChangedEvent -= ChecklistIsSelectedChangedEvent;

        if (!Checklists.Remove(checklist))
        {
            return false;
        }

        await _checklistServices.DeleteChecklistAsync(checklistId);

        return true;
    }


    #endregion

    #region - Private Methods -

    /// <summary>
    /// Load the checklists data
    /// </summary>
    public async Task LoadChecklistsAsync()
    {
        Checklists = await GetChecklistsAsync();
        RegisterChecklistDisplayModelEvents(Checklists);
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
    /// Register the event handlers for the given list of checklists
    /// </summary>
    /// <param name="checklists"></param>
    private void RegisterChecklistDisplayModelEvents(IEnumerable<ChecklistSidebarDisplayModel> checklists)
    {
        foreach (var checklist in checklists)
        {
            checklist.IsSelectedChangedEvent += ChecklistIsSelectedChangedEvent;
        }
    }

    /// <summary>
    /// Event handler for when a checklist is selected
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void ChecklistIsSelectedChangedEvent(object? sender, EventArgs args)
    {
        if (sender is ChecklistSidebarDisplayModel checklist)
        {
            ChecklistSelectedEvent?.Invoke(this, checklist);
        }
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



    private ChecklistSidebarDisplayModel GetChecklistById(Guid checklistId)
    {
        return Checklists.Where(c => c.Model.Id == checklistId).First();
    }


    private void SetChecklistIsSelected(Guid checklistId, bool isSelected)
    {
        var checklist = GetChecklistById(checklistId);
        SetIsSelectedQuietly(checklist, isSelected);
    }

    private void SetIsSelectedQuietly(ChecklistSidebarDisplayModel checklist, bool isSelected)
    {
        checklist.IsSelectedChangedEvent -= ChecklistIsSelectedChangedEvent;
        checklist.IsSelected = isSelected;
        checklist.IsSelectedChangedEvent += ChecklistIsSelectedChangedEvent;
    }


    #endregion







}
