using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Services.Interfaces;

namespace Tasks.WpfUi.ViewModels.Controls;

public partial class OpenChecklistViewModel : ObservableObject
{
    #region - Private Members -
    private readonly IChecklistServices _checklistServices = App.GetService<IChecklistServices>();
    #endregion

    #region - Public Properties -
    public Guid ChecklistId { get; private set; }
    public bool IsChecklistLoaded => Checklist != null;
    #endregion

    #region - Events -
    public event EventHandler<Guid> CloseOpenChecklistEvent;
    public event EventHandler<Guid> OpenChecklistSettingsPageEvent;
    public event EventHandler<Guid> DeleteOpenChecklistEvent;
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

    #endregion

    #region - Initialization -

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="checklistId"></param>
    public OpenChecklistViewModel(Guid checklistId)
    {
        ChecklistId = checklistId;
    }

    #endregion

    #region - Commands -

    /// <summary>
    /// CloseButtonClickedCommand
    /// </summary>
    [RelayCommand]
    private void CloseButtonClicked()
    {
        CloseOpenChecklistEvent?.Invoke(this, ChecklistId);
    }

    /// <summary>
    /// OpenChecklistSettingsPageCommand
    /// </summary>
    [RelayCommand]
    private void OpenChecklistSettingsPage()
    {
        OpenChecklistSettingsPageEvent?.Invoke(this, ChecklistId);
    }

    /// <summary>
    /// DeleteOpenChecklistCommand
    /// </summary>
    [RelayCommand]
    private void DeleteOpenChecklist()
    {
        DeleteOpenChecklistEvent?.Invoke(this, ChecklistId);
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

        IsProgressBarVisible = false;
    }

    #endregion
}
