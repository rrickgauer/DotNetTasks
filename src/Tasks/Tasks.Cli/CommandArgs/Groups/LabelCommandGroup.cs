using System.CommandLine;
using Tasks.Cli.Binders.Label;
using Tasks.Cli.CommandArgs.Helpers;
using Tasks.Cli.Controllers;
using Tasks.Service.CustomAttributes;

namespace Tasks.Cli.CommandArgs.Groups;

public class LabelCommandGroup : CommandGroup
{
    private readonly LabelController _labelController;

    public static Option<uint?> IndexOption { get; } = new("--label", "Label index");
    public static Option<string?> NameOption { get; } = new("--name", "Label name");
    public static Option<string?> ColorOption { get; } = new("--color", "Label color");
    public static Option<bool> ForceOption { get; } = SharedComponents.GetForceOption();

    public override Command TopLevelCommand { get; } = new("label", "View your labels");

    [SubCommand]
    [AddOption(nameof(NameOption))]
    [AddOption(nameof(ColorOption))]
    public Command NewCommand { get; } = new("new");

    [SubCommand]
    [AddOption(nameof(NameOption))]
    [AddOption(nameof(ColorOption))]
    [AddOption(nameof(IndexOption))]
    public Command EditCommand { get; } = new("edit");

    [SubCommand]
    [AddOption(nameof(IndexOption))]
    [AddOption(nameof(ForceOption))]
    public Command DeleteCommand { get; } = new("delete");


    public Command ListCommand { get; } = new("list", "List all the labels");


    public LabelCommandGroup(LabelController labelController)
    {
        _labelController = labelController;
    }

    protected override void RegisterHandlers()
    {
        NewCommand.SetHandler(_labelController.RouteAsync, new NewLabelArgsBinder(NameOption, ColorOption));
        EditCommand.SetHandler(_labelController.RouteAsync, new EditLabelArgsBinder(IndexOption, NameOption, ColorOption));
        DeleteCommand.SetHandler(_labelController.RouteAsync, new DeleteLabelArgsBinder(IndexOption, ForceOption));
        TopLevelCommand.SetHandler(_labelController.RouteAsync, new ListLabelArgsBinder());
        ListCommand.SetHandler(_labelController.RouteAsync, new ListLabelArgsBinder());
    }
}
