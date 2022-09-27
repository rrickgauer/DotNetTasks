using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.ViewModels;

public partial class RecurrencesPageViewModel : ObservableObject, INavigationAware
{
    public void OnNavigatedFrom()
    {
        //throw new NotImplementedException();
    }

    public void OnNavigatedTo()
    {
        //throw new NotImplementedException();
    }

    [ObservableProperty]
    private string _text = "wtf this is weird";

}
