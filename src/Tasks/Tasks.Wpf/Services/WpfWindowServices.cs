using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Wpf.Windows;

namespace Tasks.Wpf.Services;

public class WpfWindowServices
{
    public LoginWindow LoginWindow { get; set; }
    public ContainerWindow ContainerWindow { get; set; }

    public WpfWindowServices(LoginWindow loginWindow, ContainerWindow containerWindow)
    {
        LoginWindow = loginWindow;
        ContainerWindow = containerWindow;

        LoginWindow.LoginAttempted += OnLoginAttempted;
    }

    private void OnLoginAttempted(object? sender, EventArgs e)
    {
        var vm = LoginWindow.ViewModel;

        OpenContainerWindow();
    }

    private void OpenContainerWindow()
    {
        LoginWindow.Hide();
        ContainerWindow.Show();
    }



}
