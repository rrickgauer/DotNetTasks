using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Service.Services.Interfaces;
using Tasks.WpfUi.DisplayModels;
using Tasks.WpfUi.Services;
using Tasks.WpfUi.Views.Pages;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using static Tasks.WpfUi.Messaging.Messages;

namespace Tasks.WpfUi.ViewModels.Pages.ChecklistSettings;

public partial class ChecklistSettingsLabelsViewModel : ObservableObject, IChecklistSettings,
    IRecipient<OpenChecklistSettingsPageMessage>
{

    private readonly ILabelServices _labelServices;
    private readonly IChecklistLabelServices _checklistLabelServices;
    private readonly WpfApplicationServices _applicationServices;
    private readonly INavigation _navigation;

    private Guid _currentUserId => _applicationServices.CurrentUserId;
    private Guid _checklistId = Guid.Empty;

    #region - Generated Properties -

    [ObservableProperty]
    private ObservableCollection<ChecklistLabelDisplayModel> _labels = new();

    /// <summary>
    /// IsLoading
    /// </summary>
    [ObservableProperty]
    private bool _isLoading = false;

    #endregion


    public ChecklistSettingsLabelsViewModel(ILabelServices labelServices, IChecklistLabelServices checklistLabelServices, WpfApplicationServices applicationServices, INavigationService navigationService)
    {
        _labelServices = labelServices;
        _checklistLabelServices = checklistLabelServices;
        _applicationServices = applicationServices;
        _navigation = navigationService.GetNavigationControl();

        RegisterMessenger();
    }



    #region - INavigationAware -

    public void OnNavigatedFrom()
    {
        //throw new NotImplementedException();
    }

    public async void OnNavigatedTo()
    {
        IsLoading = true;
        await LoadAssignmentsAsync();
        IsLoading = false;
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
            CleanUp();
            RegisterMessenger();
        }

    }

    public void CleanUp()
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);
        WeakReferenceMessenger.Default.Cleanup();
    }

    #endregion


    #region - Messager Handling -

    public void Receive(OpenChecklistSettingsPageMessage message)
    {
        _checklistId = message.Value;
    }
    #endregion


    #region - Commands -

    [RelayCommand]
    private void Assign(ChecklistLabelDisplayModel label)
    {
        SaveAssignmentAsync(label);
    }

    [RelayCommand]
    private void EditLabels()
    {
        _navigation.Navigate(typeof(LabelsPage));
    }

    #endregion



    #region - Private Methods -

    private async Task LoadAssignmentsAsync()
    {
        var displayModels = await GetUserChecklistLabelDisplayModelsAsync();
        var assignedLabelIds = await GetAssignedLabelIdsAsync();

        foreach (var displayModel in displayModels)
        {
            if (assignedLabelIds.Contains(displayModel.Model.Id))
            {
                displayModel.IsAssigned = true;
            }
            else
            {
                displayModel.IsAssigned = false;
            }
        }

        Labels = new(displayModels);
    }

    /// <summary>
    /// Get all the user's labels as Display models
    /// </summary>
    /// <returns></returns>
    private async Task<List<ChecklistLabelDisplayModel>> GetUserChecklistLabelDisplayModelsAsync()
    {
        var userLabels = (await _labelServices.GetLabelsAsync(_currentUserId));
        var displayModels = userLabels.Select(l => new ChecklistLabelDisplayModel(l));

        return displayModels.ToList();
    }

    /// <summary>
    /// Get a list of all the assigned labels that the current checklist has assigned to it
    /// </summary>
    /// <returns></returns>
    private async Task<List<Guid?>> GetAssignedLabelIdsAsync()
    {
        var checklistLabels = await _checklistLabelServices.GetAssignedLabelsAsync(_checklistId);
        var assignedLabelIds = checklistLabels.Select(l => l.Id);

        return assignedLabelIds.ToList();
    }


    private async Task SaveAssignmentAsync(ChecklistLabelDisplayModel label)
    {
        if (label.IsAssigned)
        {
            await AssignLabelAsync(label.Model.Id.Value);
        }
        else
        {
            await DeleteAssignmentAsync(label.Model.Id.Value);
        }
    }

    private async Task AssignLabelAsync(Guid labelId)
    {
        await _checklistLabelServices.SaveAsync(new()
        {
            ChecklistId = _checklistId,
            LabelId = labelId,
            CreatedOn = DateTime.UtcNow,
        });
    }
    
    private async Task DeleteAssignmentAsync(Guid labelId)
    {
        await _checklistLabelServices.DeleteAsync(new()
        {
            ChecklistId = _checklistId,
            LabelId = labelId,
        });
    }


    #endregion


}
