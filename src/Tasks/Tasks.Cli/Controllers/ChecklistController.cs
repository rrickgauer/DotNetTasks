using Spectre.Console;
using Spectre.Console.Rendering;
using Tasks.Service.Domain.CliArgs.Cli.Checklist;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Services.Implementations;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Utilities;


using ChecklistsSelection = Spectre.Console.SelectionPrompt<Tasks.Service.Domain.TableView.ChecklistView>;

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


    private static ChecklistsSelection ChecklistsSelectionPrompt => new ChecklistsSelection()
        .Title("Which checklist")
        .UseConverter(c => c.Title ?? string.Empty);

    public ChecklistController(IChecklistServices checklistServices, WpfApplicationServices wpfApplicationServices, IConsoleServices consoleServices)
    {
        _checklistServices = checklistServices;
        _applicationServices = wpfApplicationServices;
        _consoleServices = consoleServices;
    }

    /// <summary>
    /// Clone checklist
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task RouteAsync(CloneChecklistArgs args)
    {
        var checklists = await GetCurrentChecklistsAsync();

        // prompt client for any missing arguments needed to perform the clone
        args.Title ??= AnsiConsole.Ask<string>("New checklist title: ");
        args.Index ??= _consoleServices.GetSelectedPromptIndex(ChecklistsSelectionPrompt, checklists);

        var checklistIdToCopy = checklists.ElementAt(args.Index.Value).Id.Value;

        var clonedChecklist = await _checklistServices.CopyChecklistAsync(checklistIdToCopy, args.Title);

        _consoleServices.PrintJsonObject(clonedChecklist);
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
        await DisplayChecklistsTableAsync();
    }

    /// <summary>
    /// Get a list of all the user's current checklists
    /// </summary>
    /// <returns></returns>
    private async Task<List<ChecklistView>> GetCurrentChecklistsAsync()
    {
        var checklists = await _checklistServices.GetUserChecklistsAsync(_userId);

        return checklists.ToList();
    }


    private async Task DisplayChecklistsTableAsync()
    {

        // get the user's checklists
        var checklists = await _checklistServices.GetUserChecklistsAsync(_userId);

        // generate the output table
        var table = _consoleServices.GetTableWithIndex(checklists);

        // Render the table to the console
        AnsiConsole.Write(table);
    }
}
