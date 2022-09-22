using Microsoft.Extensions.DependencyInjection;
using Tasks.Auth;
using Tasks.Configurations;
using Tasks.Repositories.Implementations;
using Tasks.Repositories.Interfaces;
using Tasks.Services.Implementations;
using Tasks.Services.Interfaces;

namespace Tasks.DependenciesInjector;

public static class ServicesInjector
{
    /// <summary>
    /// Inject all the dependencies
    /// </summary>
    /// <param name="builder"></param>
    public static void InjectDependencies(IServiceCollection services, bool isDevlopment)
    {
        // set the appropriate configuration class
        // depends if the app is running in development or production
        if (isDevlopment)
        {
            services.AddSingleton<IConfiguration, ConfigurationDev>();
        }
        else
        {
            services.AddSingleton<IConfiguration, ConfigurationProduction>();
        }


        // services
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
        .AddScoped<IEventLabelRepository, EventLabelRepository>()

        // custom filters
        .AddScoped<CustomHeaderFilter>();
    }
}
