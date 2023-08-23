using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tasks.WpfUi.ViewModels.Controls;
using Wpf.Ui.Common.Interfaces;

namespace Tasks.WpfUi.Views.Controls;

/// <summary>
/// Interaction logic for ChecklistsSidebarControl.xaml
/// </summary>
public partial class ChecklistsSidebarControl : UserControl, INavigableView<ChecklistsSidebarViewModel>
{
    public ChecklistsSidebarViewModel ViewModel { get; set; }

    public ChecklistsSidebarControl()
    {
        ViewModel = App.GetService<ChecklistsSidebarViewModel>();
        DataContext = this;

        InitializeComponent();
    }


}
