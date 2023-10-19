using Microsoft.Extensions.DependencyInjection;
using Tasks.Cli.CommandArgs;
using Tasks.Cli.Controllers;
using Tasks.Service.DependenciesInjector;
using Tasks.Service.Services.Implementations;
using Tasks.Service.Services.Interfaces;

namespace Tasks.Cli.Services;

public class CliServicesInjector : ServicesInjector
{
    public CliServicesInjector(bool isDebug) : base(isDebug)
    {

    }

    protected override void InjectAdditionalDependencies(IServiceCollection services)
    {
        services
            .AddSingleton<WpfApplicationServices>()
            .AddSingleton<IConsoleServices, ConsoleServices>()

            // command groups
            .AddSingleton<TasksRootCommand>()
            .AddSingleton<ChecklistCommandGroup>()
            .AddSingleton<LabelCommandGroup>()

            // controllers
            .AddSingleton<AppController>()
            .AddSingleton<ChecklistController>()
            .AddSingleton<LabelController>();

    }
}
