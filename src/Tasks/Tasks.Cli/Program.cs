using Microsoft.Extensions.DependencyInjection;
using Tasks.Cli.CommandArgs;
using Tasks.Cli.Controllers;
using Tasks.Service.DependenciesInjector;



ServiceCollection serviceCollection = new();

bool isDebug = false;

#if DEBUG
isDebug = true;
#endif

ServicesInjector.InjectDependencies(serviceCollection, isDebug);

#region - Dependency Injection -

serviceCollection
    // command groups
    .AddSingleton<TasksRootCommand>()
    .AddSingleton<ChecklistCommandGroup>()
    .AddSingleton<LabelCommandGroup>()

    // controllers
    .AddSingleton<AppController>()
    .AddSingleton<ChecklistController>()
    .AddSingleton<LabelController>();

#endregion

var services = serviceCollection.BuildServiceProvider();

var appController = services.GetRequiredService<AppController>();

return await appController.RunApp(args);

