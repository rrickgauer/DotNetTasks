using Microsoft.Extensions.DependencyInjection;
using Tasks.Cli.CommandArgs;
using Tasks.Cli.Controllers;
using Tasks.Service.DependenciesInjector;
using Tasks.Service.Services.Implementations;
using Tasks.Service.Services.Interfaces;

ServiceCollection serviceCollection = new();

bool isDebug = false;

#if DEBUG
isDebug = true;
#endif

ServicesInjector.InjectDependencies(serviceCollection, isDebug);

#region - Dependency Injection -

serviceCollection
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

#endregion

var services = serviceCollection.BuildServiceProvider();


var appServices = services.GetRequiredService<WpfApplicationServices>();
var credentials = await appServices.GetUserCredentials() ?? throw new Exception("Please log in via the GUI");
await appServices.LogInUser(credentials.Email, credentials.Password);

var appController = services.GetRequiredService<AppController>();

var ser = services.GetRequiredService<IChecklistServices>();

//var checklists = await ser.GetUserChecklistsAsync(appServices.CurrentUserId);

int sss = 1;

return await appController.RunApp(args);

