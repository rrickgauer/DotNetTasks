using Microsoft.Extensions.DependencyInjection;

namespace Tasks.Reminders;

public class RemindersController
{
    private readonly TasksRemindersCliArgs _cliArgs;

    private bool _isDevelopment => _cliArgs.Development;

    public RemindersController(TasksRemindersCliArgs cliArgs)
    {
        _cliArgs = cliArgs;
    }

    public IServiceProvider GetServiceProvider()
    {
        RemindersServicesInjector servicesInjector = new(_isDevelopment);

        return servicesInjector.BuildServices();
    }
}
