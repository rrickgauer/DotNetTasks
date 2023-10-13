using Spectre.Console;
using Spectre.Console.Rendering;
using Tasks.Service.Domain.CliArgs.Cli.Checklist;
using Tasks.Service.Domain.Models;
using Tasks.Service.Services.Implementations;
using Tasks.Service.Services.Interfaces;

namespace Tasks.Cli.Controllers;

public class ChecklistController : 
    IRouteAsync<CloneChecklistArgs>,
    IRouteAsync<DeleteChecklistArgs>,
    IRouteAsync<EditChecklistArgs>,
    IRouteAsync<ViewChecklistArgs>,
    IRouteAsync<NewChecklistArgs>
{

    private readonly IChecklistServices _checklistServices;
    private readonly WpfApplicationServices _applicationServices;
    private readonly IConsoleServices _consoleServices;

    private Guid _userId => _applicationServices.CurrentUserId;

    public ChecklistController(IChecklistServices checklistServices, WpfApplicationServices wpfApplicationServices, IConsoleServices consoleServices)
    {
        _checklistServices = checklistServices;
        _applicationServices = wpfApplicationServices;
        _consoleServices = consoleServices;
    }

    public async Task RouteAsync(CloneChecklistArgs args)
    {
        Console.WriteLine("CloneChecklistArgs");
    }

    public async Task RouteAsync(DeleteChecklistArgs args)
    {
        Console.WriteLine("DeleteChecklistArgs");
    }

    public async Task RouteAsync(EditChecklistArgs args)
    {
        Console.WriteLine("EditChecklistArgs");
    }

    public async Task RouteAsync(NewChecklistArgs args)
    {
        Console.WriteLine("NewChecklistArgs");
    }

    /// <summary>
    /// View all the user's checklists
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task RouteAsync(ViewChecklistArgs args)
    {
        // get the user's checklists
        var checklists = await _checklistServices.GetUserChecklistsAsync(_userId);
     
        // generate the output table
        var table = _consoleServices.GetTableWithIndex(checklists);

        // Render the table to the console
        AnsiConsole.Write(table);

    }
}
