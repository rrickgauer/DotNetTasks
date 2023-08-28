using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Tasks.Service.Domain.Models;
using Tasks.Service.Services.Interfaces;
using Tasks.WpfUi.Helpers.ModelForms;
using Tasks.WpfUi.Messaging;
using Tasks.WpfUi.Services;
//using System.Threading.Tasks;

namespace Tasks.WpfUi.ViewModels.Controls;

public partial class ChecklistItemViewModel : ObservableObject, ITaskMessenger, IModelForm<ChecklistItem>
{
    #region - Private Members -
    private readonly IChecklistItemServices _checklistItemServices = App.GetService<IChecklistItemServices>();
    private readonly CustomAlertServices _customAlertServices = App.GetService<CustomAlertServices>();
    private ChecklistItem _checklistItem;
    #endregion

    #region - Public Properties -

    public ChecklistItem ChecklistItem
    {
        get => BuildModel();
        set => SetPropertyValues(value);
    }

    #endregion



    #region - Generated Properties -

    /// <summary>
    /// Content
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SaveChangesCommand))]
    [property: ModelFormProperty(nameof(ChecklistItem.Content))]
    private string _content = string.Empty;

    /// <summary>
    /// IsComplete
    /// </summary>
    [ObservableProperty]
    [property: ModelFormProperty(nameof(ChecklistItem.IsComplete))]
    private bool _isComplete = false;


    [ObservableProperty]
    private bool _displayEditForm = false;

    #endregion




    #region - Init -

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="checklistItemId"></param>
    public ChecklistItemViewModel(ChecklistItem checklistItem)
    {
        ChecklistItem = checklistItem;
        RegisterMessenger();
    }

    #endregion


    #region - Commands -

    /// <summary>
    /// EditCommand
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private async Task Edit()
    {
        ToggleEditFormVisibility();
    }

    /// <summary>
    /// DeleteCommand
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private async Task Delete()
    {
        int x = 10;
    }

    /// <summary>
    /// Save changes command
    /// </summary>
    /// <returns></returns>
    [RelayCommand(CanExecute = nameof(CanSaveChanges))]
    private async Task SaveChangesAsync()
    {
        await UpdateItemAsync();
        
        DisplayEditForm = false;


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


    #region - IModelForm -
    public ChecklistItem BuildModel()
    {
        ModelFormUtilities.SetModelPropertyValues(this, _checklistItem);
        return _checklistItem;
    }

    public void SetPropertyValues(ChecklistItem model)
    {
        _checklistItem = model;
        ModelFormUtilities.SetFormPropertyValues(model, this);
    }

    public List<PropertyInfo> GetPropertiesToCopy() => ModelFormUtilities.GetModelFormProperties<ChecklistItemViewModel>();
    #endregion



    #region - Private Methods -

    private void ToggleEditFormVisibility()
    {
        DisplayEditForm = !DisplayEditForm;
    }

    private bool CanSaveChanges()
    {
        if (string.IsNullOrWhiteSpace(Content))
        {
            return false;
        }

        return true;
    }


    private async Task UpdateItemAsync()
    {
        await _checklistItemServices.SaveChecklistItemAsync(ChecklistItem);
    }


    #endregion



}
