using Microsoft.Extensions.DependencyInjection;
using Tasks.Cli.CommandArgs;
using Tasks.Cli.Controllers;
using Tasks.Service.DependenciesInjector;


#region - Dependency Injection -
ServiceCollection serviceCollection = new();

ServicesInjector.InjectDependencies(serviceCollection, true);

serviceCollection.AddSingleton<TasksRootCommand>();
serviceCollection.AddSingleton<ChecklistCommandGroup>();
serviceCollection.AddSingleton<TasksRootCommand>();
serviceCollection.AddSingleton<AppController>();
serviceCollection.AddSingleton<ChecklistController>();

#endregion

var services = serviceCollection.BuildServiceProvider();

var appController = services.GetRequiredService<AppController>();

return await appController.RunApp(args);

