using System.CommandLine;
using Tasks.Cli.Binders.Checklist;
using Tasks.Cli.Controllers;
using Tasks.Service.CustomAttributes;

namespace Tasks.Cli.CommandArgs;

public class ChecklistCommandGroup : CommandGroupBase
{
    private readonly ChecklistController _checklistController;

    [AddOption(nameof(IndexArgument))]
    public override Command TopLevelCommand { get; protected set; } = new("checklist");


    [SubCommand]
    [AddOption(nameof(TitleOption))]
    public Command NewCommand { get; private set; } = new("new", "Create a new checklist");

    [SubCommand]
    [AddOption(nameof(TitleOption))]
    [AddOption(nameof(IndexArgument))]
    public Command EditCommand { get; private set; } = new("edit", "Edit an existing checklist");

    [SubCommand]
    [AddOption(nameof(IndexArgument))]
    [AddOption(nameof(ForceOption))]
    public Command DeleteCommand { get; private set; } = new("delete", "Delete a checklist");

    [SubCommand]
    [AddOption(nameof(TitleOption))]
    [AddOption(nameof(IndexArgument))]
    public Command CloneCommand { get; private set; } = new("clone", "Clone a checklist");


    public static Argument<uint?> IndexArgument { get; private set; } = new("index", "Checklist index");
    public static Option<string?> TitleOption { get; private set; } = new("--title", "The checklist title");
    public static Option<bool> ForceOption { get; private set; } = new("--force", getDefaultValue: () => false, "Force the deletion");


    public ChecklistCommandGroup(ChecklistController checklistController)
    {
        _checklistController = checklistController;
    }

    protected override void RegisterHandlers()
    {
        CloneCommand.SetHandler(_checklistController.RouteAsync, new CloneChecklistArgsBinder());
        EditCommand.SetHandler(_checklistController.RouteAsync, new EditChecklistArgsBinder());
        NewCommand.SetHandler(_checklistController.RouteAsync, new NewChecklistArgsBinder());
        DeleteCommand.SetHandler(_checklistController.RouteAsync, new DeleteChecklistArgsBinder());
        TopLevelCommand.SetHandler(_checklistController.RouteAsync, new ViewChecklistArgsBinder());
    }

}
