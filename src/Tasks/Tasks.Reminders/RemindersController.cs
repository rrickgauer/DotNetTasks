﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Configurations;
using Tasks.Service.Repositories.Implementations;
using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Services.Implementations;
using Tasks.Service.Services.Interfaces;

namespace Tasks.Reminders;

public class RemindersController
{

    private readonly TasksRemindersCliArgs _cliArgs;

    private bool _isDevelopment => _cliArgs.Development;

    public RemindersController(TasksRemindersCliArgs cliArgs)
    {
        _cliArgs = cliArgs;
    }

    public ServiceProvider GetServiceProvider()
    {
        ServiceCollection serviceCollection = new();

        InjectConfig(serviceCollection);

        serviceCollection
            .AddScoped<IRemiderServices, ReminderServices>()
            .AddScoped<IEventServices, EventServices>()
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

        return serviceCollection.BuildServiceProvider();
    }

    /// <summary>
    /// set the appropriate configuration class
    /// depends if the app is running in development or production
    /// </summary>
    /// <param name="services"></param>
    private void InjectConfig(ServiceCollection services)
    {
        if (_isDevelopment)
        {
            services.AddSingleton<IConfigs, ConfigurationDev>();
        }
        else
        {
            services.AddSingleton<IConfigs, ConfigurationProduction>();
        }
    }


}
