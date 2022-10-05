using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Services.Interfaces;
using Tasks.WpfUi.Services;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace Tasks.WpfUi.ViewModels;

public partial class AssignedEventLabelsViewModel : ObservableObject, INavigationAware
{
    private readonly INavigation _navigation = App.GetService<INavigationService>().GetNavigationControl();

    private readonly WpfApplicationServices _applicationServices;
    private readonly IEventLabelServices _eventLabelServices;


    /// <summary>
    /// Contains the previous page from which this page was navigated to
    /// </summary>
    private INavigationItem? _previousNavigationItem = null;

    public AssignedEventLabelsViewModel(WpfApplicationServices applicationServices, IEventLabelServices eventLabelServices)
    {
        _applicationServices = applicationServices;
        _eventLabelServices = eventLabelServices;
    }



    #region INavigationAware
    public void OnNavigatedFrom()
    {
        //throw new NotImplementedException();
    }

    public void OnNavigatedTo()
    {
        _previousNavigationItem = _navigation.Current;  // Record the page from which this page was navigated from
    }
    #endregion


    [RelayCommand]
    public void GoBack()
    {
        if (_previousNavigationItem is null) return;

        _navigation.Navigate(_previousNavigationItem.PageTag);
    }


}
