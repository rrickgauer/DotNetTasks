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
            // App Host
            services.AddHostedService<ApplicationHostService>();

            // Page resolver service
            services.AddSingleton<IPageService, PageService>();

            // Theme manipulation
            services.AddSingleton<IThemeService, ThemeService>();

            // TaskBar manipulation
            services.AddSingleton<ITaskBarService, TaskBarService>();

            // Service containing navigation, same as INavigationWindow... but without window
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<ISnackbarService, SnackbarService>();

            services.AddSingleton<CustomAlertServices>();

            // Main window container with navigation
            services.AddScoped<INavigationWindow, Views.Container>();
            services.AddScoped<ContainerViewModel>();

            #region Pages and ViewModels

            // dashboard page (login)
            services.AddScoped<Views.Pages.DashboardPage>();
            services.AddScoped<DashboardViewModel>();

            // settings
            services.AddScoped<Views.Pages.SettingsPage>();
            services.AddScoped<SettingsViewModel>();

            // Recurrences
            services.AddScoped<Views.Pages.RecurrencesPage>();
            services.AddScoped<RecurrencesPageViewModel>();

            // Labels
            services.AddScoped<Views.Pages.LabelsPage>();
            services.AddScoped<LabelsPageViewModel>();

            // Edit label
            services.AddScoped<Views.Pages.EditLabelPage>();
            services.AddScoped<EditLabelViewModel>();

            // View event
            services.AddScoped<Views.Pages.ViewEventPage>();
            services.AddScoped<ViewEventPageViewModel>();

            // Assigned labels
            services.AddScoped<Views.Pages.AssignedEventLabelsPage>();
            services.AddScoped<AssignedEventLabelsViewModel>();

            // Account
            services.AddScoped<Views.Pages.AccountPage>();
            services.AddScoped<AccountPageViewModel>();

            // Home
            services.AddScoped<Views.Pages.HomePage>();
            services.AddScoped<HomePageViewModel>();

            services.AddScoped<Views.Pages.Checklists.ChecklistsPage>();
            services.AddScoped<ChecklistsViewModel>();

            services.AddScoped<ChecklistSettingsContainerPage>();
            services.AddScoped<ChecklistSettingsContainerViewModel>();

            services.AddScoped<ChecklistSettingsGeneralViewModel>();
            services.AddScoped<ChecklistSettingsGeneralPage>();

            services.AddScoped<ChecklistSettingsLabelsPage>();
            services.AddScoped<ChecklistSettingsLabelsViewModel>();

            services.AddScoped<ChecklistSettingsItemsPage>();
            services.AddScoped<ChecklistSettingsItemsViewModel>();


            services.AddScoped<ChecklistsSidebarViewModel>();

            #endregion

            services.AddScoped<WpfApplicationServices>();


            if (_cliArgs.Debug)
            {
                ServicesInjector.InjectDependencies(services, true);
            }
            else
            {
                ServicesInjector.InjectDependencies(services, false);
            }

            // Configuration
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