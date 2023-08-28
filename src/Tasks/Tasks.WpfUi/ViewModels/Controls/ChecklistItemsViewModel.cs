using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Service.Services.Interfaces;
using Tasks.WpfUi.Messaging;
using Tasks.WpfUi.Views.Controls;

namespace Tasks.WpfUi.ViewModels.Controls;

public partial class ChecklistItemsViewModel : ObservableObject, ITaskMessenger
{

    #region - Private Members -
    private readonly IChecklistItemServices _checklistItemServices = App.GetService<IChecklistItemServices>();
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

    public ChecklistItemsViewModel(Guid checklistId)
    {
        ChecklistId = checklistId;
        
        RegisterMessenger();
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

        var controls = items.Select(i => new ChecklistItemControl(new(i))).ToList();

        return new(controls);
    }

    #endregion

}
