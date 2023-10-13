using System.CommandLine;
using Tasks.Cli.CommandArgs;

namespace Tasks.Cli.Controllers;

public class AppController
{
    private readonly TasksRootCommand _rootCommand;

    public AppController(TasksRootCommand rootCommand)
    {
        _rootCommand = rootCommand;
    }

    public async Task<int> RunApp(string[] args)
    {
        _rootCommand.SetupCommandGroups();

        return await _rootCommand.InvokeAsync(args);
    }


}
