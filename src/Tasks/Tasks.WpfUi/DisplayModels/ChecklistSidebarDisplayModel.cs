#pragma warning disable CS8629 // Nullable value type may be null.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using Tasks.Service.Domain.TableView;
using Tasks.WpfUi.Messaging;
using Tasks.WpfUi.Services;
using static Tasks.WpfUi.Messaging.Messages;

namespace Tasks.WpfUi.DisplayModels;

public partial class ChecklistSidebarDisplayModel : ObservableObject, IDisplayModel<ChecklistView>, ITaskMessenger
{
    private bool _ignoreIsSelectedChange = false;

    public ChecklistView Model { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="model"></param>
    public ChecklistSidebarDisplayModel(ChecklistView model)
    {
        Model = model;
    }

    /// <summary>
    /// IsSelected
    /// </summary>
    [ObservableProperty]
    private bool _isSelected = false;


    partial void OnIsSelectedChanged(bool value)
    {
        if (_ignoreIsSelectedChange)
        {
            return;
        }

        if (value)
        {
            TaskMessengerServices.Send(new OpenChecklistControlMessage(Model.Id.Value));
        }
        else
        {
            TaskMessengerServices.Send(new CloseOpenChecklistMessage(Model.Id.Value));
        }
    }

    public void SetIsSelectedQuietly(bool isSelected)
    {
        _ignoreIsSelectedChange = true;
        IsSelected = isSelected;
        _ignoreIsSelectedChange = false;
    }


    #region - Display Title -

    private const int MaxDisplayLength = 30;

    public string DisplayTitle
    {
        get
        {
            if (Model.Title?.Length <= MaxDisplayLength)
            {
                return Model.Title;
            }

            return $"{Model.Title?.Substring(0, MaxDisplayLength)}...";
        }
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




}
