using Microsoft.Extensions.DependencyInjection;
using Tasks.Service.DependenciesInjector;
using Tasks.Service.Services.Implementations;
using Tasks.Service.Services.Interfaces;

namespace Tasks.Reminders;

public class RemindersServicesInjector : ServicesInjector
{
    public RemindersServicesInjector(bool isDebug) : base(isDebug)
    {
        // nothing to do
    }

    protected override void InjectAdditionalDependencies(IServiceCollection services)
    {
        services.AddScoped<IRemiderServices, ReminderServices>();
    }
}
