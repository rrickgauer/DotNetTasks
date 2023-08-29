using Tasks.WpfUi.Messaging;
using Tasks.WpfUi.ViewModels.Pages.ChecklistSettings;
using Wpf.Ui.Common.Interfaces;

using SettingsControl = Tasks.WpfUi.Views.Pages.Checklists.ChecklistSettings.IChecklistSettingsPage<Tasks.WpfUi.ViewModels.Pages.ChecklistSettings.IChecklistSettings>;

namespace Tasks.WpfUi.Views.Pages.Checklists.ChecklistSettings;

/// <summary>
/// Interaction logic for ChecklistSettingsContainerPage.xaml
/// </summary>
public partial class ChecklistSettingsContainerPage : INavigableView<ChecklistSettingsContainerViewModel>
{
    public ChecklistSettingsContainerViewModel ViewModel { get; set; }

    private readonly SettingsControl GeneralPage = App.GetService<ChecklistSettingsGeneralPage>();
    private readonly SettingsControl LabelsPage = App.GetService<ChecklistSettingsLabelsPage>();
    private readonly SettingsControl ItemsPage = App.GetService<ChecklistSettingsItemsPage>();

    public ChecklistSettingsContainerPage(ChecklistSettingsContainerViewModel viewModel)
    {
        ViewModel = viewModel;

        ViewModel.SelectedPageChangedEvent += OnSelectedPageChangedEvent;

        //GeneralPage.ViewModel.RegisterMessenger();
        //LabelsPage.ViewModel.RegisterMessenger();
        //ItemsPage.ViewModel.RegisterMessenger();


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

            case Helpers.ChecklistSettingsPages.Items:
                SettingsFrame.Navigate(ItemsPage);
                ItemsPage.ViewModel.OnNavigatedTo();
                break;
        }
    }
}
