﻿using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Tasks.Service.Services.Implementations;
using Tasks.WpfUi.Views;
using Wpf.Ui.Mvvm.Contracts;

namespace Tasks.WpfUi.Services;

/// <summary>
/// Managed host of the application.
/// </summary>
public class ApplicationHostService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private INavigationWindow _navigationWindow;
    private readonly WpfApplicationServices _applicationServices;

    public ApplicationHostService(IServiceProvider serviceProvider, WpfApplicationServices applicationServices)
    {
        _serviceProvider = serviceProvider;
        _applicationServices = applicationServices;
    }

    /// <summary>
    /// Triggered when the application host is ready to start the service.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await HandleActivationAsync();
    }

    /// <summary>
    /// Triggered when the application host is performing a graceful shutdown.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    /// <summary>
    /// Creates main window during activation.
    /// </summary>
    private async Task HandleActivationAsync()
    {
        await Task.CompletedTask;

        if (!Application.Current.Windows.OfType<Container>().Any())
        {
            _navigationWindow = (_serviceProvider.GetService(typeof(INavigationWindow)) as INavigationWindow)!;
            _navigationWindow!.ShowWindow();

            //_navigationWindow.Navigate(typeof(Views.Pages.DashboardPage));
            _navigationWindow.Navigate(typeof(Views.Pages.DashboardPage));
        }

        await Task.CompletedTask;
    }
}
