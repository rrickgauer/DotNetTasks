using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using Tasks.WpfUi.Models;
using Tasks.WpfUi.Services;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;
using Tasks.Service.DependenciesInjector;
using Tasks.WpfUi.ViewModels.Pages;
using Tasks.WpfUi.ViewModels.Controls;
using Tasks.WpfUi.ViewModels.Pages.ChecklistSettings;
using Tasks.WpfUi.Views.Pages.Checklists.ChecklistSettings;
using Tasks.WpfUi.Messaging;
using Tasks.Service.Domain.CliArgs.Exe;
using Tasks.Service.Services.Implementations;

namespace Tasks.WpfUi;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{

    // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    private static IHost _host;

    private static IHost GetHost()
    {
        return Host
        .CreateDefaultBuilder()
        .ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)); })
        .ConfigureServices((context, services) =>
        {
            WpfUiServicesInjector servicesInjector = new(_cliArgs.Debug, services);
            servicesInjector.BuildServices();

            services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
        }).Build();
    }

    private static WpfUiCliArgs _cliArgs;


    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null"/>.</returns>
    public static T GetService<T>() where T : class
    {
        return _host.Services.GetService(typeof(T)) as T;
    }

    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private async void OnStartup(object sender, StartupEventArgs e)
    {
        _cliArgs = Parser.Default.ParseArguments<WpfUiCliArgs>(e.Args).Value;

        _host = GetHost();

        await _host.StartAsync();

        // save the cli args that were passed into the application
        var applicationServices = GetService<WpfApplicationServices>();
        applicationServices.CliArgs = e.Args.ToList();

        ITaskMessenger checklistSettingsViewModel = GetService<ChecklistSettingsContainerViewModel>();
        checklistSettingsViewModel.RegisterMessenger();

        GetService<ChecklistSettingsGeneralViewModel>().RegisterMessenger();
        GetService<ChecklistSettingsLabelsViewModel>().RegisterMessenger();
        GetService<ChecklistSettingsItemsViewModel>().RegisterMessenger();
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();

        _host.Dispose();
    }

    /// <summary>
    /// Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
    }
}