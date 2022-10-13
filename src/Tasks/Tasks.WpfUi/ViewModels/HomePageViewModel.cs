using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using Tasks.WpfUi.Services;
using Tasks.WpfUi.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace Tasks.WpfUi.ViewModels;

public partial class HomePageViewModel : ObservableObject, INavigationAware
{
    #region Private members
    private readonly WpfApplicationServices _applicationServices;
    private readonly INavigationService _navigationService;

    private readonly RecurrencesPage _recurrencesPage = App.GetService<IPageService>().GetPage<RecurrencesPage>();
    private readonly LabelsPage _labelsPage           = App.GetService<IPageService>().GetPage<LabelsPage>();
    private readonly AccountPage _accountPage         = App.GetService<IPageService>().GetPage<AccountPage>();
    private readonly SettingsPage _settingsPage       = App.GetService<IPageService>().GetPage<SettingsPage>();
    #endregion


    public enum PageNames
    {
        Recurrences,
        Labels,
        Account,
        Settings,
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="applicationServices"></param>
    /// <param name="navigationService"></param>
    public HomePageViewModel(WpfApplicationServices applicationServices, INavigationService navigationService)
    {
        _applicationServices = applicationServices;
        _navigationService = navigationService;
    }


    [RelayCommand]
    public void GoToPage(PageNames page)
    {

        Type pageType = page switch
        {
            PageNames.Recurrences => _recurrencesPage.GetType(),
            PageNames.Labels => _labelsPage.GetType(),
            PageNames.Account => _accountPage.GetType(),
            PageNames.Settings => _settingsPage.GetType(),
            _ => _recurrencesPage.GetType(),
        };

        _navigationService.Navigate(pageType); 
    }

    #region INavigationAware
    public void OnNavigatedFrom()
    {
        //throw new NotImplementedException();
    }

    public void OnNavigatedTo()
    {
        //throw new NotImplementedException();
    }
    #endregion
}
