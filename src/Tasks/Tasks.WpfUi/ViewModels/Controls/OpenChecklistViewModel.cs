using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Threading.Tasks;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Services.Interfaces;
using Tasks.WpfUi.Messaging;
using Tasks.WpfUi.Services;
using Tasks.WpfUi.Views.Controls;
using static Tasks.WpfUi.Messaging.Messages;

namespace Tasks.WpfUi.ViewModels.Controls;

public partial class OpenChecklistViewModel : ObservableObject, ITaskMessenger
{
    #region - Private Members -
    private readonly IChecklistServices _checklistServices = App.GetService<IChecklistServices>();
    private readonly IChecklistItemServices _checklistItemServices = App.GetService<IChecklistItemServices>();
    private ChecklistItemsViewModel ChecklistItemsViewModel => ChecklistItemsControl.ViewModel;

    //private readonly Guid _messengerToken = new(@"f99abb5a-ba56-4a37-858c-ea280c226b56");
    private readonly Guid _messengerToken = Guid.NewGuid();

    #endregion

    #region - Public Properties -
    public Guid ChecklistId { get; private set; }
    public bool IsChecklistLoaded => Checklist != null;

    #endregion

    #region - Generated Properties -

    /// <summary>
    /// Checklist
    /// </summary>
    [ObservableProperty]
    private ChecklistView? _checklist = null;

    /// <summary>
    /// IsProgressBarVisible
    /// </summary>
    [ObservableProperty]
    private bool _isProgressBarVisible = true;


    /// <summary>
    /// ChecklistItemsControl
    /// </summary>
    [ObservableProperty]
    private ChecklistItemsControl _checklistItemsControl;

    /// <summary>
    /// NewItemText
    /// </summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(NewItemCommand))]
    private string _newItemText = string.Empty;

    #endregion

    #region - Initialization -

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="checklistId"></param>
    public OpenChecklistViewModel(Guid checklistId)
    {
        ChecklistId = checklistId;

        ChecklistItemsControl = new(new(checklistId, _messengerToken));

        RegisterMessenger();
    }

    #endregion

    #region - Commands -

    /// <summary>
    /// CloseButtonClickedCommand
    /// </summary>
    [RelayCommand]
    private void CloseButtonClicked()
    {
        TaskMessengerServices.Send(new CloseOpenChecklistMessage(ChecklistId));
    }

    /// <summary>
    /// OpenChecklistSettingsPageCommand
    /// </summary>
    [RelayCommand]
    private void OpenChecklistSettingsPage()
    {
        TaskMessengerServices.Send(new OpenChecklistSettingsPageMessage(ChecklistId));
    }

    /// <summary>
    /// DeleteOpenChecklistCommand
    /// </summary>
    [RelayCommand]
    private void DeleteOpenChecklist()
    {
        TaskMessengerServices.Send(new DeleteOpenChecklistMessage(ChecklistId));
    }

    [RelayCommand(CanExecute = nameof(CanCreateNewChecklistItem))]
    private async Task NewItemAsync()
    {
        await ChecklistItemsViewModel.AddNewChecklistItemAsync(NewItemText);
        NewItemText = string.Empty;
    }


    #endregion

    #region - ITaskMessenger -
    public void RegisterMessenger()
    {
        //WeakReferenceMessenger.Default.RegisterAll(this);
        WeakReferenceMessenger.Default.RegisterAll(this, _messengerToken);
    }

    public void CleanUp()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this, _messengerToken);
        WeakReferenceMessenger.Default.Cleanup();
    }

    #endregion

    #region - Public Methods -

    /// <summary>
    /// Load the checklist's data
    /// </summary>
    /// <returns></returns>
    public async Task LoadChecklistData()
    {
        IsProgressBarVisible = true;

        Checklist = await _checklistServices.GetChecklistAsync(ChecklistId);

        await ChecklistItemsViewModel.LoadChecklistItemsAsync();

        IsProgressBarVisible = false;
    }

    #endregion



    #region - Private Methods -


    private bool CanCreateNewChecklistItem()
    {
        if (string.IsNullOrWhiteSpace(NewItemText))
        {
            return false;
        }

        return true;
    }


    #endregion

}
