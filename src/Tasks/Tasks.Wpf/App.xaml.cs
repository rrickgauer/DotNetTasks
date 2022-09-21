using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tasks.DependenciesInjector;
using Tasks.Wpf.ViewModels;
using Tasks.Wpf.Windows;

namespace Tasks.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected async override void OnStartup(StartupEventArgs e)
    {
        //LoginWindow window = new();
        //window.Show();

        ServiceCollection services = new();
        ServicesInjector.InjectDependencies(services, true);

        services.AddSingleton<LoginWindowViewModel>();
        services.AddTransient<LoginWindow>();

        var builder = services.BuildServiceProvider();
        var loginWindow = builder.GetRequiredService<LoginWindow>();

        loginWindow.Show();

    }
}
