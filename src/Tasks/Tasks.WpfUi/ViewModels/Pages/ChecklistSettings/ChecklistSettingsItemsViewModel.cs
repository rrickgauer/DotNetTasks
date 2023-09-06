using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Threading.Tasks;
using System.Windows;
using Tasks.Service.Services.Interfaces;
using Tasks.WpfUi.Services;
using Tasks.WpfUi.ViewModels.Controls;
using Tasks.WpfUi.Views.Controls;
using static Tasks.WpfUi.Messaging.Messages;

namespace Tasks.WpfUi.ViewModels.Pages.ChecklistSettings;

public partial class ChecklistSettingsItemsViewModel : ObservableObject, IChecklistSettings,
    IRecipient<OpenChecklistSettingsPageMessage>
{

    #region - Private Members
    private readonly IChecklistItemServices _checklistItemServices;
    private readonly CustomAlertServices _customAlertServices;

    private Guid _checklistId = Guid.Empty;
    private Guid _messengerToken = Guid.NewGuid();

    private ChecklistItemsViewModel ChecklistItemsViewModel => _checklistItemsControl.ViewModel;
    #endregion



    #region - Generated Properties -

    /// <summary>
    /// IsSpinnerVisible
    /// </summary>
    [ObservableProperty]
    private bool _isSpinnerVisibile = false;

    /// <summary>
    /// ChecklistIUtemsControl
    /// </summary>
    [ObservableProperty]
    private ChecklistItemsControl _checklistItemsControl;

    /// <summary>
    /// NewItemInputText
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(NewItemCommand))]
    private string _newItemInputText = string.Empty;


    #endregion

    #region - Initialization

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="checklistItemServices"></param>
    public ChecklistSettingsItemsViewModel(IChecklistItemServices checklistItemServices, CustomAlertServices customAlertServices) 
    {
        _checklistItemServices = checklistItemServices;
        _customAlertServices = customAlertServices;
    }

    #endregion


    #region - INavigationAware -

    public void OnNavigatedFrom()
    {
        IsSpinnerVisibile = true;
    }

    public async void OnNavigatedTo()
    {
        await LoadItemsAsync();
    }

    #endregion


    #region - ITaskMessenger -

    public void RegisterMessenger()
    {
        try
        {
            WeakReferenceMessenger.Default.RegisterAll(this);
            WeakReferenceMessenger.Default.RegisterAll(this, _messengerToken);
        }
        catch(InvalidOperationException)
        {
            // idk
        }

    }

    public void CleanUp()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);
        WeakReferenceMessenger.Default.UnregisterAll(this, _messengerToken);
    }

    #endregion


    #region - Messages -

    public void Receive(OpenChecklistSettingsPageMessage message)
    {
        _checklistId = message.Value;
    }


    #endregion



    #region - Commands -

    /// <summary>
    /// NewItemCommand
    /// </summary>
    /// <returns></returns>
    [RelayCommand(CanExecute = nameof(CanNewItem))]
    private async Task NewItemAsync()
    {
        await CreateNewItemAsync();
    }


    [RelayCommand]
    private async Task ExportAsync()
    {
        var data = await _checklistItemServices.GetExportItemsStringAsync(_checklistId);
        
        Clipboard.SetText(data);

        _customAlertServices.Successful("Items copied to clipboard");
    }



    #endregion


    #region - Can Execute -

    private bool CanNewItem()
    {
        if (string.IsNullOrWhiteSpace(NewItemInputText))
        {
            return false;
        }

        return true;
    }



    #endregion



    #region - Private Methods -

    /// <summary>
    /// Load the checklist items
    /// </summary>
    /// <returns></returns>
    private async Task LoadItemsAsync()
    {
        IsSpinnerVisibile = true;

        ChecklistItemsControl = new(new(_checklistId, _messengerToken));
        await ChecklistItemsViewModel.LoadChecklistItemsAsync();

        IsSpinnerVisibile = false;
    }

    /// <summary>
    /// Create a new item
    /// </summary>
    /// <returns></returns>
    private async Task CreateNewItemAsync()
    {
        await ChecklistItemsViewModel.AddNewChecklistItemAsync(NewItemInputText);
        NewItemInputText = string.Empty;
    }

    #endregion







}
