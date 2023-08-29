using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;

namespace Tasks.WpfUi.ViewModels.Pages.ChecklistSettings;

public partial class ChecklistSettingsItemsViewModel : ObservableObject, IChecklistSettings
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

        try
        {
            WeakReferenceMessenger.Default.RegisterAll(this);
        }
        catch(InvalidOperationException)
        {
            // idk
        }

    }

    public void CleanUp()
    {
        //throw new NotImplementedException();
    }

    #endregion
}
