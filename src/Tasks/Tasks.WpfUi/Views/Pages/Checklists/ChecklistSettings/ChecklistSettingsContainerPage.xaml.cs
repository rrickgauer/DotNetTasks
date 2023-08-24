using Tasks.WpfUi.ViewModels.Pages.ChecklistSettings;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Pages.Checklists.ChecklistSettings;

/// <summary>
/// Interaction logic for ChecklistSettingsContainerPage.xaml
/// </summary>
public partial class ChecklistSettingsContainerPage : INavigableView<ChecklistSettingsContainerViewModel>
{
    public ChecklistSettingsContainerViewModel ViewModel { get; set; }

    public ChecklistSettingsGeneralPage GeneralPage { get; } = App.GetService<ChecklistSettingsGeneralPage>();
    public ChecklistSettingsLabelsPage LabelsPage { get; } = App.GetService<ChecklistSettingsLabelsPage>();

    public ChecklistSettingsContainerPage(ChecklistSettingsContainerViewModel viewModel)
    {
        ViewModel = viewModel;

        ViewModel.SelectedPageChangedEvent += OnSelectedPageChangedEvent;

        InitializeComponent();
    }

    private void OnSelectedPageChangedEvent(object? sender, Helpers.ChecklistSettingsPages e)
    {
        switch (e)
        {
            case Helpers.ChecklistSettingsPages.General:
                SettingsFrame.Navigate(GeneralPage);
                GeneralPage.ViewModel.OnNavigatedTo();
                break;

            case Helpers.ChecklistSettingsPages.Labels:
                SettingsFrame.Navigate(LabelsPage);
                LabelsPage.ViewModel.OnNavigatedTo();
                break;
        }
    }
}
