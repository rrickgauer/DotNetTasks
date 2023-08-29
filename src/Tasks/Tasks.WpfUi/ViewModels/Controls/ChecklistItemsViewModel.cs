using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Service.Services.Interfaces;
using Tasks.WpfUi.Messaging;
using Tasks.WpfUi.Services;
using Tasks.WpfUi.Views.Controls;
using static Tasks.WpfUi.Messaging.Messages;

namespace Tasks.WpfUi.ViewModels.Controls;

public partial class ChecklistItemsViewModel : ObservableObject, ITaskMessenger,
    IRecipient<OpenChecklistItemDeletedMessage>
{

    #region - Private Members -
    private readonly IChecklistItemServices _checklistItemServices = App.GetService<IChecklistItemServices>();
    private readonly CustomAlertServices _customAlertServices = App.GetService<CustomAlertServices>();

    private readonly Guid _messengerToken;
    #endregion

    #region - Public Properties -
    public Guid ChecklistId { get; private set; }
    #endregion


    #region - Generated Properties -

    /// <summary>
    /// Items
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<ChecklistItemControl> _items = new();

    #endregion


    #region - Init -

    public ChecklistItemsViewModel(Guid checklistId, Guid messengerToken)
    {
        ChecklistId = checklistId;
        _messengerToken = messengerToken;
        
        RegisterMessenger();
    }

    #endregion


    #region - ITaskMessenger -
    public void RegisterMessenger()
    {
        WeakReferenceMessenger.Default.RegisterAll(this, _messengerToken);

        //WeakReferenceMessenger.Default.RegisterAll()
    }

    public void CleanUp()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this, _messengerToken);
        WeakReferenceMessenger.Default.Cleanup();
    }

    #endregion



    #region - Messenger Handlers -

    public void Receive(OpenChecklistItemDeletedMessage message)
    {
        var itemId = message.Value;
        RemoveChecklistItemControl(itemId);
    }


    #endregion



    #region - Public Methods -

    public async Task LoadChecklistItemsAsync()
    {
        Items = await GetItemControlsAsync();
    }




    #endregion

    #region - Private Methods -

    /// <summary>
    /// Get a list of the checklist items.
    /// </summary>
    /// <returns></returns>
    private async Task<ObservableCollection<ChecklistItemControl>> GetItemControlsAsync()
    {
        var items = await _checklistItemServices.GetChecklistItemsAsync(ChecklistId);

        var controls = items.Select(i => new ChecklistItemControl(new(i, _messengerToken))).ToList();

        return new(controls);
    }


    private void RemoveChecklistItemControl(Guid checklistItemId)
    {
        var control = GetChecklistItemControl(checklistItemId);

        control.ViewModel.CleanUp();

        Items.Remove(control);
    }
    

    private ChecklistItemControl GetChecklistItemControl(Guid checklistItemId)
    {
        var control = Items.Where(i => i.ViewModel.ChecklistItemId == checklistItemId).First();

        return control;
    }


    #endregion

}
