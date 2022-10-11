using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.WpfUi.Services;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.ViewModels;

public partial class AccountPageViewModel : ObservableObject, INavigationAware
{
    #region INavigationAware
    public void OnNavigatedFrom()
    {
        //throw new NotImplementedException();
    }

    public void OnNavigatedTo()
    {
        //LoadLabelsAsync();
    }
    #endregion


    private readonly WpfApplicationServices _applicationServices;

    public AccountPageViewModel(WpfApplicationServices applicationServices)
    {
        _applicationServices = applicationServices;
    }


}
