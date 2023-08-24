#pragma warning disable CS8629 // Nullable value type may be null.

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using Tasks.Service.Domain.TableView;

namespace Tasks.WpfUi.DisplayModels;

public partial class ChecklistSidebarDisplayModel : ObservableObject, IDisplayModel<ChecklistView>
{
    public ChecklistView Model { get; set; }

    public ChecklistSidebarDisplayModel(ChecklistView model)
    {
        Model = model;
    }


    public event EventHandler<EventArgs> IsSelectedChangedEvent;


    [ObservableProperty]
    private bool _isSelected = false;


    partial void OnIsSelectedChanged(bool value)
    {
        IsSelectedChangedEvent?.Invoke(this, EventArgs.Empty);
    }

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



}
