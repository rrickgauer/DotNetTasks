using Spectre.Console;
using Tasks.Service.Domain.CliArgs.Cli.Checklist;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Services.Implementations;
using Tasks.Service.Services.Interfaces;
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

        args.CommandLineId ??= _consoleServices.GetSelectedPrompt(ChecklistsSelectionPrompt, checklists).CommandLineReferenceId.Value;

        var checklistToCopy = checklists.Where(c => c.CommandLineReferenceId == args.CommandLineId.Value).First();

        var clonedChecklist = await _checklistServices.CopyChecklistAsync(checklistToCopy.Id.Value, args.Title);

        //_consoleServices.PrintJsonObject(clonedChecklist);
    }



    /// <summary>
    /// Delete the checklist
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task RouteAsync(DeleteChecklistArgs args)
    {
        var checklists = await GetCurrentChecklistsAsync();
        
        args.CommandLineId ??= _consoleServices.GetSelectedPrompt(ChecklistsSelectionPrompt, checklists).CommandLineReferenceId.Value;

        if (!ConfirmDelete(args))
        {
            Console.WriteLine("Checklist NOT deleted!");
            return;
        }

        //var checklistId = GetChecklistIdFromIndex(checklists, args.CommandLineId.Value);
        var checklistId = checklists.Where(c => c.CommandLineReferenceId == args.CommandLineId.Value).First().Id.Value;

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

        //args.CommandLineId ??= _consoleServices.GetSelectedPromptIndex(ChecklistsSelectionPrompt, checklists);
        args.CommandLineId ??= _consoleServices.GetSelectedPrompt(ChecklistsSelectionPrompt, checklists).CommandLineReferenceId;

        //var existingChecklist = (Checklist)checklists[args.CommandLineId.Value];
        Checklist existingChecklist = (Checklist)checklists.Where(c => c.CommandLineReferenceId == args.CommandLineId).First();

        args.Title ??= AnsiConsole.Ask("Updated title: ", existingChecklist.Title ?? string.Empty);
        
        existingChecklist.Title = args.Title;

        await _checklistServices.SaveChecklistAsync(existingChecklist);

        _consoleServices.DisplayCommandSuccess();
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

        _consoleServices.DisplayCommandSuccess();
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
        var table = _consoleServices.BuildTable(checklists.OrderBy(c => c.CommandLineReferenceId));

        // Render the table to the console
        AnsiConsole.Write(table);
    }
}
