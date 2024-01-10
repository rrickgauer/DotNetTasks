using System.CommandLine;
using System.CommandLine.Binding;
using Tasks.Cli.CommandArgs.Groups;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.CliArgs.Cli.ChecklistItem;
using static Tasks.Service.Domain.CliArgs.Cli.Contracts.ChecklistItemCliContracts;
using static Tasks.Service.Domain.CliArgs.Cli.Contracts.CommonCliContracts;

namespace Tasks.Cli.Binders.ChecklistItem;

public class DeleteChecklistItemArgsBinder : ArgsBinderBase<DeleteChecklistItemArgs>, IValueDescriptor<DeleteChecklistItemArgs>
{
    [CopyTo(nameof(IChecklistItemCliChecklistId.ChecklistCommandLineId))]
    public Option<uint> ChecklistIdOption { get; set; } = ChecklistItemCommandGroup.ChecklistIdOption;

    [CopyTo(nameof(IChecklistItemCliIndex.ItemCommandLineId))]
    public Option<uint?> ItemReferenceArgument { get; set; } = ChecklistItemCommandGroup.ItemReferenceOption;

    [CopyTo(nameof(ICliDeleteFlag.Force))]
    public Option<bool> ForceDeleteOption { get; set; } = ChecklistItemCommandGroup.ForceDeleteOption;
}
