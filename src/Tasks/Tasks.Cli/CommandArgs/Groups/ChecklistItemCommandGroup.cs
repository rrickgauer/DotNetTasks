using System.CommandLine;
using Tasks.Cli.Binders.ChecklistItem;
using Tasks.Cli.CommandArgs.Helpers;
using Tasks.Cli.Controllers;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.Enums;

namespace Tasks.Cli.CommandArgs.Groups;

public class ChecklistItemCommandGroup : CommandGroup
{
    #region - Private Members -

    private readonly ChecklistItemController _controller;

    #endregion

    #region - Options -

    public static Option<uint> ChecklistIdOption { get; } = new("--checklist", "The checklist id")
    {
        IsRequired = true,
    };

    public static Option<CliChecklistItemStatus?> StatusOption { get; } = new(name: "--status", 
        description: "update status",
        parseArgument: result =>
        {
            if (result.Tokens.Count == 0)
            {
                return null;
            }

            string enumValueText = result.Tokens.First().Value;
            return Enum.Parse<CliChecklistItemStatus>(enumValueText, true);
        })
    {
        Arity = ArgumentArity.ZeroOrOne,
    };

    public static Option<bool> ForceDeleteOption { get; } = SharedComponents.GetForceOption();
    public static Option<bool> NoPromptsOption { get; } = SharedComponents.NoPromptsOption();

    public static Option<CliDataOutputStyle> TableOutputStyleOption { get; } = SharedComponents.GetTableOutputStyleOption();

    public static Option<string?> ContentOption { get; } = new("--content", "The content of the item");
    public static Option<uint?> ItemReferenceOption { get; } = new("--item", "The item id");

    public static Option<bool> InteractiveOption { get; } = new("--interactive", () => false, "Interact with the items");
    public static Option<bool> NoStatusPromptOption { get; } = new("--no-status", () => false, "Don't prompt for status");
    public static Option<bool> NoContentPromptOption { get; } = new("--no-content", () => false, "Don't prompt for item content");
    public static Option<bool> NoItemPromptOption { get; } = new("--no-item", () => false, "Don't prompt for item");

    #endregion

    #region - Commands -

    public override Command TopLevelCommand { get; } = new("checklist-item", "Interact with your checklists' items");


    /// <summary>
    /// checklist-item [new] {--content}
    /// </summary>
    [SubCommand]
    [AddOption(nameof(ContentOption))]
    public Command NewCommand { get; } = SharedComponents.GetNewCommand("Create a new item");

    /// <summary>
    /// checklist-item --checklist= [edit] --item= --content= --status={toggle,complete,incomplete} --no-prompts --no-content --no-status --no-item
    /// </summary>
    [SubCommand]
    [AddOption(nameof(ItemReferenceOption))]
    [AddOption(nameof(ContentOption))]
    [AddOption(nameof(StatusOption))]
    [AddOption(nameof(NoPromptsOption))]
    [AddOption(nameof(NoContentPromptOption))]
    [AddOption(nameof(NoStatusPromptOption))]
    [AddOption(nameof(NoItemPromptOption))]
    public Command EditCommand { get; } = SharedComponents.GetEditCommand("Edit the item");


    /// <summary>
    /// checklist-item --checklist= [delete] --item= --force
    /// </summary>
    [SubCommand]
    [AddOption(nameof(ItemReferenceOption))]
    [AddOption(nameof(ForceDeleteOption))]
    public Command DeleteCommand { get; } = SharedComponents.GetDeleteCommand("Delete the item");


    /// <summary>
    /// checklist-item --checklist= [list] --output={default,none,markdown,json} --interactive
    /// </summary>
    [SubCommand]
    [AddOption(nameof(InteractiveOption))]
    [AddOption(nameof(TableOutputStyleOption))]
    public Command ListCommand { get; protected set; } = new("list", "View all the items");

    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="checklistItemController"></param>
    public ChecklistItemCommandGroup(ChecklistItemController checklistItemController)
    {
        _controller = checklistItemController;
    }


    /// <summary>
    /// Register all the sub command handlers
    /// </summary>
    protected override void RegisterHandlers()
    {
        TopLevelCommand.AddGlobalOption(ChecklistIdOption);

        NewCommand.SetHandler(_controller.RouteAsync, new NewChecklistItemArgsBinder());
        EditCommand.SetHandler(_controller.RouteAsync, new EditChecklistItemArgsBinder());
        DeleteCommand.SetHandler(_controller.RouteAsync, new DeleteChecklistItemArgsBinder());
        ListCommand.SetHandler(_controller.RouteAsync, new ListChecklistItemArgsBinder());
    }
}
