using System.CommandLine;
using System.CommandLine.Binding;
using Tasks.Cli.CommandArgs.Groups;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.CliArgs.Cli.ChecklistItem;
using Tasks.Service.Domain.Enums;
using static Tasks.Service.Domain.CliArgs.Cli.Contracts.ChecklistItemCliContracts;

namespace Tasks.Cli.Binders.ChecklistItem;

public class ListChecklistItemArgsBinder : ArgsBinderBase<ListChecklistItemArgs>, IValueDescriptor<ListChecklistItemArgs>
{
    [CopyTo(nameof(IChecklistItemCliChecklistId.ChecklistCommandLineId))]
    public Option<uint> ChecklistIdOption { get; set; } = ChecklistItemCommandGroup.ChecklistIdOption;

    [CopyTo(nameof(ListChecklistItemArgs.Interactive))]
    public Option<bool> InteractiveOption { get; set; } = ChecklistItemCommandGroup.InteractiveOption;

    [CopyTo(nameof(ListChecklistItemArgs.Style))]
    public Option<CliDataOutputStyle> TableOutputStyleOption { get; set; } = ChecklistItemCommandGroup.TableOutputStyleOption;

    [CopyTo(nameof(ListChecklistItemArgs.FilterItems))]
    public Option<CliShowChecklistItemsOption> FilterItemsOption { get; set; } = ChecklistItemCommandGroup.FilterItemsOption;
}

