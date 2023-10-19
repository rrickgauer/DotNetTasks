using Microsoft.Extensions.DependencyInjection;
using Tasks.Service.DependenciesInjector;
using Tasks.Service.Services.Implementations;
using Tasks.WpfUi.ViewModels.Controls;
using Tasks.WpfUi.ViewModels.Pages.ChecklistSettings;
using Tasks.WpfUi.ViewModels.Pages;
using Tasks.WpfUi.Views.Pages.Checklists.ChecklistSettings;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace Tasks.WpfUi.Services;

public class WpfUiServicesInjector : ServicesInjector
{
    public WpfUiServicesInjector(bool isDebug, IServiceCollection services) : base(isDebug)
    {
        _serviceCollection = services;
    }

    protected override void InjectAdditionalDependencies(IServiceCollection services)
    {
        AddWpfUiServices(services);
        AddViews(services);
        AddMiscDependencies(services);
    }

    /// <summary>
    /// Add all the essential WpfUi dependencies
    /// </summary>
    /// <param name="services"></param>
    private static void AddWpfUiServices(IServiceCollection services)
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
        services.AddScoped<ContainerViewModel>();
    }

    /// <summary>
    /// Add all the views and ViewModels
    /// </summary>
    /// <param name="services"></param>
    private static void AddViews(IServiceCollection services)
    {
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
    }

    /// <summary>
    /// Add any additional dependencies
    /// </summary>
    /// <param name="services"></param>
    private static void AddMiscDependencies(IServiceCollection services)
    {
        services.AddSingleton<CustomAlertServices>();
        services.AddScoped<WpfApplicationServices>();
    }
}
