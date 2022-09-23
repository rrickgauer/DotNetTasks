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
using Tasks.Wpf.Pages;
using Tasks.Wpf.Services;
using Tasks.Wpf.ViewModels;
using Tasks.Wpf.Windows;

namespace Tasks.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    public static ServiceProvider Services { get; private set; }


    protected async override void OnStartup(StartupEventArgs e)
    {
        Services = BuildServiceProvider();

        var windowServices = Services.GetRequiredService<WpfWindowServices>();
        windowServices.LoginWindow.Show();
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
        .AddScoped<WpfWindowServices>()
        .AddScoped<WpfApplicationServices>()
        //.AddScoped<AccountPage>()
        //.AddScoped<EventsPage>()
        //.AddScoped<HomePage>()
        //.AddScoped<LabelsPage>()
        //.AddScoped<RecurrencesPage>()
        .AddScoped<EventsPageViewModel>()
        .AddScoped<ContainerWindowViewModel>()
        .AddScoped<RecurrencesPageViewModel>();

        var builder = services.BuildServiceProvider();
        
        return builder;
    }
}
