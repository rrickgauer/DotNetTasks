using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Tasks.WpfUi.Views.Pages;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace Tasks.WpfUi.ViewModels.Pages;

public partial class HomePageViewModel : ObservableObject, INavigationAware
{
    #region Private members
    private readonly INavigation _navigationService;
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
    public HomePageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService.GetNavigationControl();
    }


    [RelayCommand]
    public void GoToPage(PageNames page)
    {
        switch (page)
        {
            case PageNames.Recurrences:
                _navigationService.Navigate(typeof(RecurrencesPage));
                break;
            case PageNames.Labels:
                _navigationService.Navigate(typeof(LabelsPage));
                break;
            case PageNames.Account:
                _navigationService.Navigate(typeof(AccountPage));
                break;
            case PageNames.Settings:
                _navigationService.Navigate(typeof(SettingsPage));
                break;
        }
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
