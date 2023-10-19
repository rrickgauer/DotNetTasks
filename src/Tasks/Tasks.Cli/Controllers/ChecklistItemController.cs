using Tasks.Service.Domain.CliArgs.Cli.ChecklistItem;
using Tasks.Service.Domain.TableView;
using Tasks.Service.Services.Interfaces;

namespace Tasks.Cli.Controllers;

public class ChecklistItemController :
    IRouteAsync<CompleteChecklistItemArgs>,
    IRouteAsync<DeleteChecklistItemArgs>,
    IRouteAsync<EditChecklistItemArgs>,
    IRouteAsync<NewChecklistItemArgs>,
    IRouteAsync<ListChecklistItemArgs>,
    IRouteAsync<ToggleChecklistItemArgs>
{

    private readonly IChecklistItemServices _checklistItemServices;
    private readonly IConsoleServices _consoleServices;
    
    public ChecklistItemController(IChecklistItemServices checklistItemServices, IConsoleServices consoleServices)
    {
        _checklistItemServices = checklistItemServices;
        _consoleServices = consoleServices;
    }



    public async Task RouteAsync(CompleteChecklistItemArgs args)
    {
        Console.Write($"{args.GetType()}");
    }

    public async Task RouteAsync(DeleteChecklistItemArgs args)
    {
        Console.Write($"{args.GetType()}");
    }

    public async Task RouteAsync(EditChecklistItemArgs args)
    {
        Console.Write($"{args.GetType()}"); 
    }

    public async Task RouteAsync(NewChecklistItemArgs args)
    {
        Console.Write($"{args.GetType()}");
    }

    public async Task RouteAsync(ToggleChecklistItemArgs args)
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
