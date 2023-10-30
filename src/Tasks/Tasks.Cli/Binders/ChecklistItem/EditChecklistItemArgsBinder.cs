using System.CommandLine;
using System.CommandLine.Binding;
using Tasks.Cli.CommandArgs.Groups;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.CliArgs.Cli.ChecklistItem;
using Tasks.Service.Domain.Enums;
using static Tasks.Service.Domain.CliArgs.Cli.Contracts.ChecklistItemCliContracts;

namespace Tasks.Cli.Binders.ChecklistItem;

public class EditChecklistItemArgsBinder : ArgsBinderBase<EditChecklistItemArgs>, IValueDescriptor<EditChecklistItemArgs>
{
    [CopyTo(nameof(IChecklistItemCliChecklistId.ChecklistCommandLineId))]
    public Option<uint> ChecklistIdOption { get; set; } = ChecklistItemCommandGroup.ChecklistIdOption;

    [CopyTo(nameof(EditChecklistItemArgs.ItemCommandLineId))]
    public Option<uint?> ItemReferenceOption { get; set; } = ChecklistItemCommandGroup.ItemReferenceOption;

    [CopyTo(nameof(EditChecklistItemArgs.Content))]
    public Option<string?> ContentOption {get; set;} = ChecklistItemCommandGroup.ContentOption;

    [CopyTo(nameof(EditChecklistItemArgs.Status))]
    public Option<CliChecklistItemStatus?> StatusOption { get; set; } = ChecklistItemCommandGroup.StatusOption;

    [CopyTo(nameof(EditChecklistItemArgs.NoPromptsFlag))]
    public Option<bool> NoPromptsOption {get; set;} = ChecklistItemCommandGroup.NoPromptsOption;

    [CopyTo(nameof(EditChecklistItemArgs.NoContentPromptFlag))]
    public Option<bool> NoContentPromptOption { get; set; } = ChecklistItemCommandGroup.NoContentPromptOption;

    [CopyTo(nameof(EditChecklistItemArgs.NoStatusPromptFlag))]
    public Option<bool> NoStatusPromptOption {get; set;} = ChecklistItemCommandGroup.NoStatusPromptOption;

    [CopyTo(nameof(EditChecklistItemArgs.NoItemPromptFlag))]
    public Option<bool> NoItemPromptOption { get; set; } = ChecklistItemCommandGroup.NoItemPromptOption;

}

