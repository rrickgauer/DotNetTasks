using System.CommandLine;
using Tasks.Cli.Binders.ChecklistItem;
using Tasks.Cli.CommandArgs.Helpers;
using Tasks.Cli.Controllers;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.Enums;

namespace Tasks.Cli.CommandArgs.Groups;

public class ChecklistItemCommandGroup : CommandGroup
{
    private readonly ChecklistItemController _controller;


    public static Argument<uint?> ItemReferenceArgument { get; } = new("id", "The item id");

    public static Option<bool> ForceDeleteOption { get; } = SharedComponents.GetForceOption();
    public static Option<CliDataOutputStyle> TableOutputStyleOption { get; } = SharedComponents.GetTableOutputStyleOption();

    public static Option<string?> ContentOption { get; } = new("--content", "The content of the item");
    public static Option<bool> IncompleteOption { get; } = new("--incomplete", getDefaultValue: () => false, "Mark the item incomplete");
    public static Option<bool> InteractiveOption { get; } = new("--interactive", getDefaultValue: () => false, "Interact with the items");
    public static Option<uint> ChecklistIdOption { get; } = new("--checklist", "The checklist id")
    {
        IsRequired = true,
    };


    
    public override Command TopLevelCommand { get; } = new("checklist-item", "Interact with your checklists' items");


    [SubCommand]
    [AddOption(nameof(ContentOption))]
    public Command NewCommand { get; } = SharedComponents.GetNewCommand("Create a new item");


    [SubCommand]
    [AddOption(nameof(ItemReferenceArgument))]
    [AddOption(nameof(ContentOption))]
    public Command EditCommand { get; } = SharedComponents.GetEditCommand("Edit the item");


    [SubCommand]
    [AddOption(nameof(ItemReferenceArgument))]
    [AddOption(nameof(ForceDeleteOption))]
    public Command DeleteCommand { get; } = SharedComponents.GetDeleteCommand("Delete the item");


    [SubCommand]
    [AddOption(nameof(ItemReferenceArgument))]
    [AddOption(nameof(IncompleteOption))]
    public Command CompleteCommand { get; } = new("complete", "Mark an item complete or incomplete");
        

    [SubCommand]
    [AddOption(nameof(ItemReferenceArgument))]
    public Command ToggleCommand { get; } = new("toggle", "Toggle the item's complete value");

    [SubCommand]
    [AddOption(nameof(InteractiveOption))]
    [AddOption(nameof(TableOutputStyleOption))]
    public Command ListCommand { get; protected set; } = new("list", "View all the items");


    public ChecklistItemCommandGroup(ChecklistItemController checklistItemController)
    {
        _controller = checklistItemController;
    }


    protected override void RegisterHandlers()
    {
        TopLevelCommand.AddGlobalOption(ChecklistIdOption);

        NewCommand.SetHandler(_controller.RouteAsync, new NewChecklistItemArgsBinder());
        EditCommand.SetHandler(_controller.RouteAsync, new EditChecklistItemArgsBinder());
        DeleteCommand.SetHandler(_controller.RouteAsync, new DeleteChecklistItemArgsBinder());
        CompleteCommand.SetHandler(_controller.RouteAsync, new CompleteChecklistItemArgsBinder());
        ToggleCommand.SetHandler(_controller.RouteAsync, new ToggleChecklistItemArgsBinder());
        ListCommand.SetHandler(_controller.RouteAsync, new ListChecklistItemArgsBinder());

    }
}
