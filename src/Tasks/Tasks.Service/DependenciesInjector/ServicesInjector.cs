using Microsoft.Extensions.DependencyInjection;
using Tasks.Service.Repositories.Implementations;
using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Auth;
using Tasks.Service.Configurations;
using Tasks.Service.Services.Implementations;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Repositories;

namespace Tasks.Service.DependenciesInjector;

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
            services.AddSingleton<IConfigs, ConfigurationDev>();
        }
        else
        {
            services.AddSingleton<IConfigs, ConfigurationProduction>();
        }


        // services
        services.AddScoped<IEventServices, EventServices>()
        .AddScoped<IRecurrenceServices, RecurrenceServices>()
        .AddScoped<IEventActionServices, EventActionServices>()
        .AddScoped<IUserServices, UserServices>()
        .AddScoped<IUserEmailVerificationServices, UserEmailVerificationServices>()
        .AddScoped<ILabelServices, LabelServices>()
        .AddScoped<IEventLabelServices, EventLabelServices>()
        .AddScoped<IChecklistServices, ChecklistServices>()
        .AddScoped<IChecklistItemServices, ChecklistItemServices>()
        .AddScoped<IChecklistLabelServices, ChecklistLabelServices>()

        .AddSingleton<IMapperServices, MapperServices>()
        

        // repositories
        .AddScoped<IUserRepository, UserRepository>()
        .AddScoped<IEventRepository, EventRepository>()
        .AddScoped<IRecurrenceRepository, RecurrenceRepository>()
        .AddScoped<IEventActionRepository, EventActionRepository>()
        .AddScoped<IUserEmailVerificationRepository, UserEmailVerificationRepository>()
        .AddScoped<ILabelRepository, LabelRepository>()
        .AddScoped<IEventLabelRepository, EventLabelRepository>()
        .AddScoped<IChecklistRepository, ChecklistRepository>()
        .AddScoped<IChecklistItemRepository, ChecklistItemRepository>()
        .AddScoped<IChecklistLabelRepository, ChecklistLabelRepository>()

        .AddTransient<DbConnection>()

        // custom filters
        .AddScoped<ChecklistAuthFilters>()
        .AddScoped<ChecklistItemAuthFilter>()
        .AddScoped<EventAuthFilter>()
        .AddScoped<LabelAuthFilter>()

        .AddScoped<CustomHeaderFilter>();
    }
}
