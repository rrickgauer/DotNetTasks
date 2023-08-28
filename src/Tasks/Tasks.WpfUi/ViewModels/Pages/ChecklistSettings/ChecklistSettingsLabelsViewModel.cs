using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.WpfUi.Helpers;
using Tasks.WpfUi.Messaging;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.ViewModels.Pages.ChecklistSettings;

public partial class ChecklistSettingsLabelsViewModel : ObservableObject, INavigationAware, ITaskMessenger
{




    public ChecklistSettingsLabelsViewModel()
    {
        RegisterMessenger();
    }



    #region - INavigationAware -

    public void OnNavigatedFrom()
    {
        //throw new NotImplementedException();
    }

    public async void OnNavigatedTo()
    {
        //await LoadChecklistData();
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
}
