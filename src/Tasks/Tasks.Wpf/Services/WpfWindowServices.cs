using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Wpf.Windows;
using Wpf.Ui.Controls;

namespace Tasks.Wpf.Services;

public class WpfWindowServices
{
    public LoginWindow LoginWindow { get; set; }
    public ContainerWindow ContainerWindow { get; set; }
    public WpfApplicationServices ApplicationServices { get; set; }

    public WpfWindowServices(LoginWindow loginWindow, ContainerWindow containerWindow, WpfApplicationServices applicationServices)
    {
        LoginWindow = loginWindow;
        ContainerWindow = containerWindow;
        ApplicationServices = applicationServices;

        LoginWindow.LoginAttempted += OnLoginAttempted;
    }

    private async void OnLoginAttempted(object? sender, EventArgs e)
    {
        var vm = LoginWindow.ViewModel;

        var successfulLogin = await ApplicationServices.LogInUser(vm.EmailTextBox ?? string.Empty, vm.PasswordTextBox ?? string.Empty);

        if (successfulLogin)
        {
            OpenContainerWindow();
            return;
        }

        MessageBox messageBox = new()
        {
            Content = "Invalid login attempt",
            ShowFooter = false,
        };



        messageBox.ShowDialog();
    }

    private void OpenContainerWindow()
    {
        LoginWindow.Hide();
        ContainerWindow.Show();
    }



}
