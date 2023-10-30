using Microsoft.Extensions.DependencyInjection;
using Tasks.Service.Auth;
using Tasks.Service.Configurations;
using Tasks.Service.Repositories;
using Tasks.Service.Repositories.Implementations;
using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Services.Implementations;
using Tasks.Service.Services.Interfaces;

namespace Tasks.Service.DependenciesInjector;

public abstract class ServicesInjector
{
    protected abstract void InjectAdditionalDependencies(IServiceCollection services);

    public bool IsDevelopment { get; private set; } = false;

    protected IServiceCollection _serviceCollection = new ServiceCollection();

    public ServicesInjector(bool isDebug)
    {
        IsDevelopment = isDebug;
    }

    public virtual IServiceProvider BuildServices()
    {
        InjectConfig();

        InjectEssentialServices();

        InjectAdditionalDependencies(_serviceCollection);

        return _serviceCollection.BuildServiceProvider();
    }

    protected void InjectConfig()
    {
        // set the appropriate configuration class
        // depends if the app is running in development or production
        if (IsDevelopment)
        {
            _serviceCollection.AddSingleton<IConfigs, ConfigurationDev>();
        }
        else
        {
            _serviceCollection.AddSingleton<IConfigs, ConfigurationProduction>();
        }
    }

    protected virtual void InjectEssentialServices()
    {
        _serviceCollection.AddScoped<IEventServices, EventServices>()
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
