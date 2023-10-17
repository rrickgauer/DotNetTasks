using Spectre.Console;
using Spectre.Console.Rendering;
using Tasks.Service.Domain.CliArgs.Cli.Checklist;
using Tasks.Service.Domain.Enums;
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

    private Guid _currentUserId => _applicationServices.CurrentUserId;


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



    /// <summary>
    /// Delete the checklist
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task RouteAsync(DeleteChecklistArgs args)
    {
        var checklists = await GetCurrentChecklistsAsync();
        
        args.Index ??= _consoleServices.GetSelectedPromptIndex(ChecklistsSelectionPrompt, checklists);

        if (!ConfirmDelete(args))
        {
            Console.WriteLine("Checklist NOT deleted!");
            return;
        }

        var checklistId = GetChecklistIdFromIndex(checklists, args.Index.Value);

        await _checklistServices.DeleteChecklistAsync(checklistId);

        Console.WriteLine($"{Environment.NewLine}Deleted!{Environment.NewLine}");

        await DisplayChecklistsTableAsync();

    }


    private Guid GetChecklistIdFromIndex(List<ChecklistView> checklists, int index)
    {
        var checklistIdToCopy = checklists.ElementAtOrDefault(index)?.Id;

        if (!checklistIdToCopy.HasValue)
        {
            throw new Exception("Invalid checklist");
        }

        return checklistIdToCopy.Value;
    }

    private bool ConfirmDelete(DeleteChecklistArgs args)
    {
        if (args.Force)
        {
            return true;
        }

        return AnsiConsole.Confirm("Are you sure you want to delete this checklist?");
    }



    public async Task RouteAsync(EditChecklistArgs args)
    {
        var checklists = await GetCurrentChecklistsAsync();

        args.Index ??= _consoleServices.GetSelectedPromptIndex(ChecklistsSelectionPrompt, checklists);


        var existingChecklist = (Checklist)checklists[args.Index.Value];

        args.Title ??= AnsiConsole.Ask("Updated title: ", existingChecklist.Title ?? string.Empty);
        
        existingChecklist.Title = args.Title;

        await _checklistServices.SaveChecklistAsync(existingChecklist);

        Console.WriteLine($"{Environment.NewLine}Success!");
    }


    /// <summary>
    /// Create a new checklist
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task RouteAsync(NewChecklistArgs args)
    {
        args.Title ??= AnsiConsole.Ask<string>("New checklist title: ");

        var newChecklistModel = CreateNewChecklistModel(args);

        var outputView = await _checklistServices.SaveChecklistAsync(newChecklistModel);

        _consoleServices.PrintJsonObject(outputView);
    }

    /// <summary>
    /// Create a new checklist model from the cli args
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private Checklist CreateNewChecklistModel(NewChecklistArgs args)
    {
        Checklist newChecklist = new()
        {
            Id        = Guid.NewGuid(),
            CreatedOn = DateTime.Now,
            ListType  = ChecklistType.List,
            Title     = args.Title,
            UserId    = _currentUserId,
        };

        return newChecklist;
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
        var checklists = await _checklistServices.GetUserChecklistsAsync(_currentUserId);

        return checklists.ToList();
    }


    private async Task DisplayChecklistsTableAsync()
    {

        // get the user's checklists
        var checklists = await _checklistServices.GetUserChecklistsAsync(_currentUserId);

        // generate the output table
        var table = _consoleServices.GetTableWithIndex(checklists);

        // Render the table to the console
        AnsiConsole.Write(table);
    }
}
