using System.CommandLine;
using Tasks.Cli.Binders.Checklist;
using Tasks.Cli.Controllers;
using Tasks.Service.CustomAttributes;

namespace Tasks.Cli.CommandArgs;

public class ChecklistCommandGroup : CommandGroupBase
{
    private readonly ChecklistController _checklistController;

    protected override Type CommandGroupType => typeof(ChecklistCommandGroup);
    
    public override Command TopLevelCommand { get; protected set; } = new("checklist");


    [SubCommand]
    public Command NewCommand { get; private set; } = new("new");

    [SubCommand]
    public Command EditCommand { get; private set; } = new("edit");

    [SubCommand]
    public Command DeleteCommand { get; private set; } = new("delete");

    [SubCommand]
    public Command CloneCommand { get; private set; } = new("clone");


    [AddToCommand(nameof(EditCommand))]
    [AddToCommand(nameof(CloneCommand))]
    [AddToCommand(nameof(DeleteCommand))]
    public static Argument<int?> IndexArgument { get; private set; } = new("Checklist Index");

    [AddToCommand(nameof(EditCommand))]
    [AddToCommand(nameof(NewCommand))]
    [AddToCommand(nameof(CloneCommand))]
    public static Option<string?> TitleOption { get; private set; } = new("--title", "The checklist title");

    [AddToCommand(nameof(DeleteCommand))]
    public static Option<bool> ForceOption { get; private set; } = new("--force", getDefaultValue: () => false, "Force the deletion");


    public ChecklistCommandGroup(ChecklistController checklistController)
    {
        _checklistController = checklistController;
    }

    protected override void RegisterHandlers()
    {
        CloneCommand.SetHandler(_checklistController.Route, new CloneChecklistArgsBinder(TitleOption, IndexArgument));
        EditCommand.SetHandler(_checklistController.Route, new EditChecklistArgsBinder(TitleOption, IndexArgument));
        NewCommand.SetHandler(_checklistController.Route, new NewChecklistArgsBinder(TitleOption));
        DeleteCommand.SetHandler(_checklistController.Route, new DeleteChecklistArgsBinder(ForceOption, IndexArgument));

    }

}
