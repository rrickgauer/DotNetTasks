#pragma warning disable CS8603 // Possible null reference return.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Utilities;
using Tasks.WpfUi.Helpers.ModelForms;
using Tasks.WpfUi.Messaging;
using Tasks.WpfUi.Services;
using Tasks.WpfUi.Views.Pages.Checklists;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using static Tasks.WpfUi.Messaging.Messages;

namespace Tasks.WpfUi.ViewModels.Pages.ChecklistSettings;

public partial class ChecklistSettingsGeneralViewModel : ObservableObject, INavigationAware, ITaskMessenger, IModelForm<ChecklistView>,
    IRecipient<OpenChecklistSettingsPageMessage>
{

    #region - Private Members -
    private readonly IChecklistServices _checklistServices;
    private readonly CustomAlertServices _customAlertServices;
    private readonly INavigation _navigationService;

    private Guid _checklistId = Guid.Empty;
    private ChecklistView? _checklist = null;

    #endregion

    #region - Public Properties -

    public IEnumerable<ChecklistType> ChecklistTypes => EnumUtilities.GetEnumEntries<ChecklistType>();

    #endregion

    #region - Generated Properties -

    /// <summary>
    /// ChecklistTitle
    /// </summary>
    [ObservableProperty]
    [property: ModelFormProperty(nameof(ChecklistView.Title))]
    [NotifyCanExecuteChangedFor(nameof(SaveGeneralSettingsChangesCommand))]
    private string _checklistTitle = string.Empty;

    /// <summary>
    /// SelectedListType
    /// </summary>
    [ObservableProperty]
    [property: ModelFormProperty(nameof(ChecklistView.ListType))]
    private ChecklistType _selectedListType = ChecklistType.List;

    /// <summary>
    /// IsProgressSpinnerShowing
    /// </summary>
    [ObservableProperty]
    private bool _isProgressSpinnerShowing = false;


    /// <summary>
    /// CloneChecklistInput
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(CloneCommand))]
    private string _cloneChecklistInput = string.Empty;

    /// <summary>
    /// OpenClonedChecklist
    /// </summary>
    [ObservableProperty]
    private bool _openClonedChecklist = false;

    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="checklistServices"></param>
    public ChecklistSettingsGeneralViewModel(IChecklistServices checklistServices, CustomAlertServices customAlertServices, INavigationService navigationService)
    {
        _checklistServices = checklistServices;
        _customAlertServices = customAlertServices;
        _navigationService = navigationService.GetNavigationControl();

        RegisterMessenger();
    }


    #region - Commands -

    /// <summary>
    /// SaveGeneralSettingsChangesCommand
    /// </summary>
    /// <returns></returns>
    [RelayCommand(CanExecute = nameof(CanSaveGeneralSettingsChangesAsync))]
    private async Task SaveGeneralSettingsChangesAsync()
    {
        IsProgressSpinnerShowing = true;
        await SaveBasicChecklistFormAsync();
        IsProgressSpinnerShowing = false;
    }

    /// <summary>
    /// CloneCommand
    /// </summary>
    /// <returns></returns>
    [RelayCommand(CanExecute = nameof(CanClone))]
    private async Task CloneAsync()
    {
        IsProgressSpinnerShowing = true;

        await CloneChecklistAsync();

        IsProgressSpinnerShowing = false;
    }


    #endregion

    #region - Can Execute -

    private bool CanSaveGeneralSettingsChangesAsync() => !string.IsNullOrWhiteSpace(ChecklistTitle);
    private bool CanClone() => !string.IsNullOrWhiteSpace(CloneChecklistInput);

    #endregion

    #region - Messenger Handling -
    public void Receive(OpenChecklistSettingsPageMessage message)
    {
        _checklistId = message.Value;
    }

    #endregion

    #region - INavigationAware -
    public void OnNavigatedFrom()
    {
        IsProgressSpinnerShowing = true;
    }

    public async void OnNavigatedTo()
    {
        IsProgressSpinnerShowing = true;
        
        await LoadChecklistDataAsync();
        CloneChecklistInput = string.Empty;
        
        IsProgressSpinnerShowing = false;

    }
    #endregion

    #region - ITaskMessenger -

    public void RegisterMessenger()
    {
        try
        {
            WeakReferenceMessenger.Default.RegisterAll(this);
        }
        catch (InvalidOperationException)
        {
            // pass
        }

    }

    public void CleanUp()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);
        WeakReferenceMessenger.Default.Cleanup();
    }

    #endregion

    #region - IModelForm -

    /// <summary>
    /// Get an updated model
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public ChecklistView BuildModel()
    {
        if (_checklist == null)
        {
            throw new NullReferenceException(nameof(_checklist));
        }

        ModelFormUtilities.SetModelPropertyValues(this, _checklist);

        return _checklist;
    }

    /// <summary>
    /// Set the property values from the checklist
    /// </summary>
    /// <param name="model"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void SetPropertyValues(ChecklistView model)
    {
        ModelFormUtilities.SetFormPropertyValues(model, this);
    }

    /// <summary>
    /// Get a list of the properties to copy
    /// </summary>
    /// <returns></returns>
    public List<PropertyInfo> GetPropertiesToCopy()
    {
        return ModelFormUtilities.GetModelFormProperties<ChecklistSettingsGeneralViewModel>();
    }

    #endregion

    #region - Private Methods -

    /// <summary>
    /// Load the checklist data
    /// </summary>
    /// <returns></returns>
    private async Task LoadChecklistDataAsync()
    {
        _checklist = await _checklistServices.GetChecklistAsync(_checklistId);

        if (_checklist != null)
        {
            SetPropertyValues(_checklist);
        }
    }

    /// <summary>
    /// Update the database with the current general settings form values
    /// </summary>
    /// <returns></returns>
    private async Task SaveBasicChecklistFormAsync()
    {
        var model = (Checklist)BuildModel();

        _checklist = await _checklistServices.SaveChecklistAsync(model);

        _customAlertServices.Successful("Changes saved!");
    }


    private async Task CloneChecklistAsync()
    {
        var clonedChecklist = await _checklistServices.CopyChecklistAsync(_checklistId, CloneChecklistInput);

        if (OpenClonedChecklist)
        {
            ViewClonedChecklist(clonedChecklist.Id.Value);
        }

        CloneChecklistInput = string.Empty;
        OpenClonedChecklist = false;

        _customAlertServices.Successful("Checklist was cloned successfully");

    }

    private void ViewClonedChecklist(Guid clonedChecklistId)
    {
        WeakReferenceMessenger.Default.Send(new OpenClonedChecklistMessage(clonedChecklistId));
        _navigationService.Navigate(typeof(ChecklistsPage));
    }



    #endregion

}
