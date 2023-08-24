using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Threading.Tasks;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Services.Interfaces;
using Tasks.WpfUi.Helpers;
using Tasks.WpfUi.Messaging;
using Wpf.Ui.Common.Interfaces;
using static Tasks.WpfUi.Messaging.Messages;

namespace Tasks.WpfUi.ViewModels.Pages.ChecklistSettings;

public partial class ChecklistSettingsGeneralViewModel : ObservableObject, INavigationAware, ITaskMessenger, IModelForm<ChecklistView>,
    IRecipient<OpenChecklistSettingsPageMessage>
{

    private readonly IChecklistServices _checklistServices;

    private Guid _checklistId = Guid.Empty;

    private ChecklistView? _checklist = null;


    /// <summary>
    /// ChecklistTitle
    /// </summary>
    [ObservableProperty]
    private string _checklistTitle = string.Empty; 



    public ChecklistSettingsGeneralViewModel(IChecklistServices checklistServices)
    {
        _checklistServices = checklistServices;

        RegisterMessenger();
    }



    





    #region - Messenger Handling -
    public void Receive(OpenChecklistSettingsPageMessage message)
    {
        _checklistId = message.Value;
    }
    #endregion


    #region - INavigationAware -
    public void OnNavigatedFrom()
    {
        //throw new NotImplementedException();
    }

    public async void OnNavigatedTo()
    {
        await LoadChecklistDataAsync();
    }
    #endregion

    #region - INavigationAware -

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


    #region - INavigationAware -

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

        ChecklistView view = new()
        {
            Title      = ChecklistTitle,
            CountItems = _checklist.CountItems,
            CreatedOn  = _checklist.CreatedOn,
            Id         = _checklistId,
            ListType   = _checklist.ListType,
            UserId     = _checklist.UserId,
        };

        return view;
    }

    /// <summary>
    /// Set the property values from the checklist
    /// </summary>
    /// <param name="model"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void SetPropertyValues(ChecklistView model)
    {
        ChecklistTitle = model.Title;
    }

    #endregion



    #region - Private Methods -

    private async Task LoadChecklistDataAsync()
    {
        _checklist = await _checklistServices.GetChecklistAsync(_checklistId);

        if (_checklist != null)
        {
            SetPropertyValues(_checklist);
        }
    }



    #endregion



}
