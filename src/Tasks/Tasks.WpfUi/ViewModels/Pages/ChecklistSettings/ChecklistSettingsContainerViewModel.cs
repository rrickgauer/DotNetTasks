using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Services.Interfaces;
using Tasks.WpfUi.Helpers;
using Tasks.WpfUi.Messaging;
using Wpf.Ui.Common.Interfaces;
using static Tasks.WpfUi.Messaging.Messages;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Controls;
using Wpf.Ui.Common;

namespace Tasks.WpfUi.ViewModels.Pages.ChecklistSettings;

public partial class ChecklistSettingsContainerViewModel : ObservableObject, INavigationAware, ITaskMessenger, IRecipient<OpenChecklistSettingsPageMessage>
{
    #region - Private Members -
    private readonly IChecklistServices _checklistServices;
    private readonly List<NavigationItem> _navigationItemsList = new();
    #endregion

    #region - Public Properties -
    public Guid ChecklistId { get; private set; } = Guid.Empty;
    public ObservableCollection<INavigationControl> NavigationItems => new(_navigationItemsList.Cast<INavigationControl>());
    #endregion

    #region - Events -
    public event EventHandler<ChecklistSettingsPages> SelectedPageChangedEvent;
    #endregion

    #region - Generated Properties -

    /// <summary>
    /// Checklist
    /// </summary>
    [ObservableProperty]
    private ChecklistView? _checklist = null;

    /// <summary>
    /// SelectedChecklistSettingsPage
    /// </summary>
    [ObservableProperty]
    private ChecklistSettingsPages _selectedChecklistSettingsPage = ChecklistSettingsPages.General;

    #endregion

    #region - Initialize -

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="checklistServices"></param>
    public ChecklistSettingsContainerViewModel(IChecklistServices checklistServices)
    {
        _checklistServices = checklistServices;

        _navigationItemsList = GetNavItems();

        RegisterMessenger();
    }

    #endregion

    #region - Message Handlers -

    public void Receive(OpenChecklistSettingsPageMessage message)
    {
        ChecklistId = message.Value;
    }

    #endregion

    #region - Commands -

    /// <summary>
    /// PageChangedCommand
    /// </summary>
    /// <param name="value"></param>
    [RelayCommand]
    private void PageChange(object value)
    {
        SelectedChecklistSettingsPage = (ChecklistSettingsPages)value;
        ActivateSelectedPage(SelectedChecklistSettingsPage);
        SelectedPageChangedEvent?.Invoke(this, SelectedChecklistSettingsPage);
    }

    #endregion

    #region - INavigationAware -

    public void OnNavigatedFrom()
    {
        //throw new NotImplementedException();
    }

    public void OnNavigatedTo()
    {
        SelectedPageChangedEvent?.Invoke(this, ChecklistSettingsPages.General);
        GetNavigationItem(ChecklistSettingsPages.General).IsActive = true;
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

    #region - Private Methods - 

    /// <summary>
    /// Get the nav items for the container
    /// </summary>
    /// <returns></returns>
    private List<NavigationItem> GetNavItems()
    {
        return new()
        {
            new NavigationItem()
            {
                Content = "General",
                PageTag = nameof(ChecklistSettingsPages.General),
                Icon = SymbolRegular.CalendarLtr32,
                Command = PageChangeCommand,
                CommandParameter = ChecklistSettingsPages.General,
            },

            new NavigationItem()
            {
                Content = "Labels",
                PageTag = nameof(ChecklistSettingsPages.Labels),
                Icon = SymbolRegular.CalendarLtr32,
                Command = PageChangeCommand,
                CommandParameter = ChecklistSettingsPages.Labels,
            },
        };
    }


    /// <summary>
    /// Get the specified navigtion item
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    private NavigationItem GetNavigationItem(ChecklistSettingsPages page)
    {
        return _navigationItemsList.Where(i => (ChecklistSettingsPages)i.CommandParameter == page).First();
    }

    /// <summary>
    /// Activate the settings page with the matching page type
    /// </summary>
    /// <param name="page"></param>
    private void ActivateSelectedPage(ChecklistSettingsPages page)
    {
        foreach (var item in _navigationItemsList)
        {
            if ((ChecklistSettingsPages)(item.CommandParameter) == page)
            {
                item.IsActive = true;
            }
            else
            {
                item.IsActive = false;
            }
        }
    }

    #endregion
}
