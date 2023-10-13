using System.CommandLine;
using Tasks.Cli.Binders.Label;
using Tasks.Cli.Controllers;
using Tasks.Service.CustomAttributes;

namespace Tasks.Cli.CommandArgs;

public class LabelCommandGroup : CommandGroupBase
{
    private readonly LabelController _labelController;

    public override Command TopLevelCommand { get; protected set; } = new("label", "Edit labels");

    protected override Type CommandGroupType => typeof(LabelCommandGroup);

    [SubCommand]
    [AddOption(nameof(NameOption))]
    [AddOption(nameof(ColorOption))]
    public Command NewCommand { get; private set; } = new("new");

    [SubCommand]
    [AddOption(nameof(NameOption))]
    [AddOption(nameof(ColorOption))]
    [AddOption(nameof(IndexArgument))]
    public Command EditCommand { get; private set; } = new("edit");

    [SubCommand]
    [AddOption(nameof(IndexArgument))]
    [AddOption(nameof(ForceOption))]
    public Command DeleteCommand { get; private set; } = new("delete");


    public static Argument<int?> IndexArgument { get; private set; } = new("Label index");
    public static Option<string?> NameOption { get; private set; }   = new("--name", "Label name");
    public static Option<string?> ColorOption { get; private set; }  = new("--color", "Label color");
    public static Option<bool> ForceOption { get; private set; }     = new("--force", getDefaultValue: () => false, "Force the deletion");

    public LabelCommandGroup(LabelController labelController)
    {
        _labelController = labelController;
    }

    protected override void RegisterHandlers()
    {
        NewCommand.SetHandler(_labelController.RouteAsync, new NewLabelArgsBinder(NameOption, ColorOption));
        EditCommand.SetHandler(_labelController.RouteAsync, new EditLabelArgsBinder(IndexArgument, NameOption, ColorOption));
        DeleteCommand.SetHandler(_labelController.RouteAsync, new DeleteLabelArgsBinder(IndexArgument, ForceOption));
        TopLevelCommand.SetHandler(_labelController.RouteAsync, new ViewAllLabelArgsBinder());
    }
}
