using Tasks.Service.DependenciesInjector;

namespace Tasks.Api.Other;

public class ApiServicesInjector : ServicesInjector
{
    public ApiServicesInjector(bool isDebug, IServiceCollection serviceCollection) : base(isDebug)
    {
        _serviceCollection = serviceCollection;
    }

    protected override void InjectAdditionalDependencies(IServiceCollection services)
    {
        // no additional
    }
}
