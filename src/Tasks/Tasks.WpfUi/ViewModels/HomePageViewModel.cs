using CommunityToolkit.Mvvm.ComponentModel;
using Tasks.WpfUi.Services;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace Tasks.WpfUi.ViewModels;

public partial class HomePageViewModel : ObservableObject, INavigationAware
{
    #region Private members
    private readonly WpfApplicationServices _applicationServices;
    private readonly INavigationService _navigationService;
    #endregion


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
