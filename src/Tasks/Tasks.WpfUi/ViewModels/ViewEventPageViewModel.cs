using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.WpfUi.Services;
using Tasks.WpfUi.Views.Pages;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace Tasks.WpfUi.ViewModels;

public partial class ViewEventPageViewModel : ObservableObject
{
    private readonly WpfApplicationServices _applicationServices;
    private readonly INavigation _navigation;
    //private readonly EditLabelPage _editLabelPage;

    public ViewEventPageViewModel(WpfApplicationServices applicationServices)
    {
        _applicationServices = applicationServices;
        _navigation = App.GetService<INavigationService>().GetNavigationControl();
        //_editLabelPage = App.GetService<IPageService>().GetPage<EditLabelPage>();
    }
}
