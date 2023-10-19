using System.CommandLine;
using Tasks.Cli.Binders.Label;
using Tasks.Cli.Controllers;
using Tasks.Service.CustomAttributes;

namespace Tasks.Cli.CommandArgs.Groups;

public class LabelCommandGroup : CommandGroup
{
    private readonly LabelController _labelController;

    public override Command TopLevelCommand { get; } = new("label", "Edit labels");

    [SubCommand]
    [AddOption(nameof(NameOption))]
    [AddOption(nameof(ColorOption))]
    public Command NewCommand { get; } = new("new");

    [SubCommand]
    [AddOption(nameof(NameOption))]
    [AddOption(nameof(ColorOption))]
    [AddOption(nameof(IndexArgument))]
    public Command EditCommand { get; } = new("edit");

    [SubCommand]
    [AddOption(nameof(IndexArgument))]
    [AddOption(nameof(ForceOption))]
    public Command DeleteCommand { get; } = new("delete");


    public static Argument<int?> IndexArgument { get; } = new("Label index");
    public static Option<string?> NameOption { get; } = new("--name", "Label name");
    public static Option<string?> ColorOption { get; } = new("--color", "Label color");
    public static Option<bool> ForceOption { get; } = new("--force", getDefaultValue: () => false, "Force the deletion");

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
