using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using Tasks.Configurations;
using Tasks.Domain.CliArgs;
using Tasks.Repositories.Implementations;
using Tasks.Repositories.Interfaces;
using Tasks.Services.Implementations;
using Tasks.Services.Interfaces;
using Tasks.WpfUi.Models;
using Tasks.WpfUi.Services;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

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

            // Main window container with navigation
            services.AddScoped<INavigationWindow, Views.Container>();
            services.AddScoped<ViewModels.ContainerViewModel>();

            #region Pages and ViewModels

            // dashboard page (login)
            services.AddScoped<Views.Pages.DashboardPage>();
            services.AddScoped<ViewModels.DashboardViewModel>();

            // settings
            services.AddScoped<Views.Pages.SettingsPage>();
            services.AddScoped<ViewModels.SettingsViewModel>();

            // Recurrences
            services.AddScoped<Views.Pages.RecurrencesPage>();
            services.AddScoped<ViewModels.RecurrencesPageViewModel>();

            // Labels
            services.AddScoped<Views.Pages.LabelsPage>();
            services.AddScoped<ViewModels.LabelsPageViewModel>();

            // Edit label
            services.AddScoped<Views.Pages.EditLabelPage>();
            services.AddScoped<ViewModels.EditLabelViewModel>();

            // View event
            services.AddScoped<Views.Pages.ViewEventPage>();
            services.AddScoped<ViewModels.ViewEventPageViewModel>();

            // Assigned labels
            services.AddScoped<Views.Pages.AssignedEventLabelsPage>();
            services.AddScoped<ViewModels.AssignedEventLabelsViewModel>();

            // Account
            services.AddScoped<Views.Pages.AccountPage>();
            services.AddScoped<ViewModels.AccountPageViewModel>();

            // Home
            services.AddScoped<Views.Pages.HomePage>();
            services.AddScoped<ViewModels.HomePageViewModel>();

            #endregion

            services.AddScoped<WpfApplicationServices>();


            if (_cliArgs.Debug)
            {
                services.AddSingleton<IConfigs, ConfigurationDev>();
            }
            else
            {
                services.AddSingleton<IConfigs, ConfigurationProduction>();
            }

            services.AddScoped<IEventServices, EventServices>()
                .AddScoped<IRecurrenceServices, RecurrenceServices>()
                .AddScoped<IEventActionServices, EventActionServices>()
                .AddScoped<IUserServices, UserServices>()
                .AddScoped<IUserEmailVerificationServices, UserEmailVerificationServices>()
                .AddScoped<ILabelServices, LabelServices>()
                .AddScoped<IEventLabelServices, EventLabelServices>()

                // repositories
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IEventRepository, EventRepository>()
                .AddScoped<IRecurrenceRepository, RecurrenceRepository>()
                .AddScoped<IEventActionRepository, EventActionRepository>()
                .AddScoped<IUserEmailVerificationRepository, UserEmailVerificationRepository>()
                .AddScoped<ILabelRepository, LabelRepository>()
                .AddScoped<IEventLabelRepository, EventLabelRepository>();



            // Configuration
            services.Configure<AppConfig>(context.Configuration.GetSection(nameof(AppConfig)));
        }).Build();
    }

    //private static IHost _host;

    private static WpfUiCliArgs _cliArgs;

    public App()
    {
        int x = 10;
    }


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

        //_host.

        // save the cli args that were passed into the application
        var applicationServices = GetService<WpfApplicationServices>();
        applicationServices.CliArgs = e.Args.ToList();
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