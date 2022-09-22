using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tasks.Wpf.ViewModels;

namespace Tasks.Wpf.Windows;


/// <summary>
/// Interaction logic for LoginWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    public LoginWindowViewModel ViewModel { get; set; }

    public event EventHandler LoginAttempted;

    public LoginWindow(LoginWindowViewModel loginWindowViewModel)
    {
        ViewModel = loginWindowViewModel;
        this.DataContext = ViewModel;

        InitializeComponent();
    }

    private void formSubmitButton_Click(object sender, RoutedEventArgs e)
    {
        LoginAttempted?.Invoke(this, new());
    }

    private void Window_Closed(object sender, EventArgs e)
    {
        Application.Current.Shutdown();
    }
}
