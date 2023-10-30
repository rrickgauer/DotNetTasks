using System.CommandLine;
using System.CommandLine.Parsing;
using Tasks.Cli.CommandArgs;
using Tasks.Service.Domain.CliArgs.Errors;
using Tasks.Service.Services.Interfaces;

namespace Tasks.Cli.Controllers;

public class AppController
{
    private readonly TasksRootCommand _rootCommand;
    private readonly IConsoleServices _consoleServices;

    public AppController(TasksRootCommand rootCommand, IConsoleServices consoleServices)
    {
        _rootCommand = rootCommand;
        _consoleServices = consoleServices;
    }

    public async Task<int> RunApp(string[] args)
    {
        _rootCommand.SetupCommandGroups();
        return await _rootCommand.InvokeAsync(args);

    }
}
