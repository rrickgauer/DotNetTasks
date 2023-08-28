using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.WpfUi.Messaging;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.ViewModels.Pages.ChecklistSettings;

public partial class ChecklistSettingsItemsViewModel : ObservableObject, INavigationAware, ITaskMessenger
{





    #region - INavigationAware -

    public void OnNavigatedFrom()
    {
        //throw new NotImplementedException();
    }

    public void OnNavigatedTo()
    {
        //throw new NotImplementedException();
    }

    #endregion


    #region - ITaskMessenger -

    public void RegisterMessenger()
    {
        //throw new NotImplementedException();
    }

    public void CleanUp()
    {
        //throw new NotImplementedException();
    }

    #endregion
}
