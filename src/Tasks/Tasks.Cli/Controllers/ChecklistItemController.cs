using Spectre.Console;
using Tasks.Service.Domain.CliArgs.Cli.ChecklistItem;
using Tasks.Service.Domain.CliArgs.Errors;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Utilities;
using static Tasks.Service.Domain.CliArgs.Cli.Contracts.ChecklistItemCliContracts;

namespace Tasks.Cli.Controllers;

public class ChecklistItemController :
    IRouteAsync<DeleteChecklistItemArgs>,
    IRouteAsync<EditChecklistItemArgs>,
    IRouteAsync<NewChecklistItemArgs>,
    IRouteAsync<ListChecklistItemArgs>
{

    private readonly IChecklistItemServices _checklistItemServices;
    private readonly IConsoleServices _consoleServices;

    private static readonly SelectionPrompt<ChecklistItemView> _itemSelectionPrompt = AnsiTableExtensions.BuildSelectionPrompt<ChecklistItemView>();

    public ChecklistItemController(IChecklistItemServices checklistItemServices, IConsoleServices consoleServices)
    {
        _checklistItemServices = checklistItemServices;
        _consoleServices = consoleServices;
    }

    public async Task RouteAsync(DeleteChecklistItemArgs args)
    {
        Console.Write($"{args.GetType()}");
    }

    public async Task RouteAsync(EditChecklistItemArgs args)
    {
        var items = await GetItemsInChecklist(args.ChecklistCommandLineId);

        if (!TryGetItemToEdit(items, args, out ChecklistItemView item))
        {
            _consoleServices.HandleCliError(new($"No item with id \"{args.ItemCommandLineId}\" exists in THIS checklist"));
            return;
        }

        if (args.ShouldPromptForContent)
        {
            args.Content = AnsiConsole.Ask<string>("Content: ");
        }

        if (!string.IsNullOrEmpty(args.Content))
        {
            item.Content = args.Content;
        }


        // check if we need to prompt for status
        if (args.ShouldPromptForStatus)
        {
            args.Status = GetStatusPromptResponse();
        }

        // update the item's status if needed
        if (args.Status.HasValue)
        {
            UpdateItemStatus(item, args.Status.Value);
        }

        await _checklistItemServices.SaveChecklistItemAsync((ChecklistItem)item);

        _consoleServices.DisplayCommandSuccess();
    }





    /// <summary>
    /// Retrieve item from either the cli argument or from list selection prompt
    /// </summary>
    /// <param name="items"></param>
    /// <param name="args"></param>
    /// <param name="foundItem"></param>
    /// <returns></returns>
    private bool TryGetItemToEdit(List<ChecklistItemView> items, EditChecklistItemArgs args, out ChecklistItemView foundItem)
    {
        if (args.ShouldPromptForItem)
        {
            args.ItemCommandLineId = _consoleServices.GetSelectedPrompt(_itemSelectionPrompt, items).CommandLineReference;
        }

        var item = items.Where(i => i.CommandLineReference == args.ItemCommandLineId).FirstOrDefault();

        foundItem = item ?? new();

        return item != null;
    }

    private static CliChecklistItemStatus? GetStatusPromptResponse()
    {
        var choices = EnumUtilities.GetEnumEntries<CliChecklistItemStatus>().Cast<CliChecklistItemStatus?>();

        var statusPrompt = new TextPrompt<CliChecklistItemStatus?>("Set item's status: ")
            .AllowEmpty()
            .AddChoices(choices)
            .HideDefaultValue()
            .DefaultValue(null)
            .WithConverter((s) => s?.ToString().ToLower() ?? string.Empty);

        return AnsiConsole.Prompt(statusPrompt);
    }


    private static void UpdateItemStatus(ChecklistItemView item, CliChecklistItemStatus status)
    {
        switch (status)
        {
            case CliChecklistItemStatus.Toggle:
                item.IsComplete = !item.IsComplete;
                break;
            case CliChecklistItemStatus.Complete:
                item.IsComplete = true;
                break;
            case CliChecklistItemStatus.Incomplete:
                item.IsComplete = false;
                break;
        }
    }



    public async Task RouteAsync(NewChecklistItemArgs args)
    {
        Console.Write($"{args.GetType()}");
    }


    public async Task RouteAsync(ListChecklistItemArgs args)
    {
        var items = await GetItemsInChecklist(args.ChecklistCommandLineId);
        _consoleServices.PrintCollection(items, args.Style);        
    }


    private async Task<List<ChecklistItemView>> GetItemsInChecklist(uint checklistCliReference)
    {
        var items = await _checklistItemServices.GetItemsByChecklistCliReferenceAsync(checklistCliReference);

        return items.ToList();
    }


}
