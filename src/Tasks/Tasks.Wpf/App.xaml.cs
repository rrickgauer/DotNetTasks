using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tasks.DependenciesInjector;
using Tasks.Services.Implementations;
using Tasks.Services.Interfaces;
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
        var builder = BuildServiceProvider();

        var loginWindow = builder.GetRequiredService<LoginWindow>();
        loginWindow.Show();
    }

    /// <summary>
    /// Setup DI for all the services
    /// </summary>
    /// <returns></returns>
    private static ServiceProvider BuildServiceProvider()
    {
        ServiceCollection services = new();
        ServicesInjector.InjectDependencies(services, true);

        services.AddScoped<LoginWindowViewModel>()
        .AddScoped<LoginWindow>()
        .AddScoped<ContainerWindow>()
        .AddScoped<IWpfApplicationServices, WpfApplicationServices>();

        var builder = services.BuildServiceProvider();
        
        return builder;
    }
}
