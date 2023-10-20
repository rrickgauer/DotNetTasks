using Microsoft.Extensions.DependencyInjection;
using Tasks.Cli.Controllers;
using Tasks.Cli.Services;
using Tasks.Service.Domain.CliArgs.Errors;
using Tasks.Service.Services.Implementations;
using Tasks.Service.Services.Interfaces;


bool isDebug = false;

#if DEBUG
isDebug = true;
#endif


CliServicesInjector servicesInjector = new(isDebug);
var services = servicesInjector.BuildServices();


var appServices = services.GetRequiredService<WpfApplicationServices>();
var credentials = await appServices.GetUserCredentials() ?? throw new Exception("Please log in via the GUI");
await appServices.LogInUser(credentials.Email, credentials.Password);

var appController = services.GetRequiredService<AppController>();

return await appController.RunApp(args);