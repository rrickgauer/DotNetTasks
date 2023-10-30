

using System.CommandLine;
using System.CommandLine.Binding;
using Tasks.Cli.CommandArgs.Groups;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.CliArgs.Cli.Checklist;

namespace Tasks.Cli.Binders.Checklist;

public class DeleteChecklistArgsBinder : ArgsBinderBase<DeleteChecklistArgs>, IValueDescriptor<DeleteChecklistArgs> 
{
    [CopyTo(nameof(DeleteChecklistArgs.Force))]
    public Option<bool> ForceOption { get; set; } = ChecklistCommandGroup.ForceOption;

    [CopyTo(nameof(DeleteChecklistArgs.CommandLineId), ParseFunctionName = nameof(BinderCasts.ParseUInt))]
    public Argument<uint?> ChecklistReferenceArgument { get; set; } = ChecklistCommandGroup.IndexArgument;
}
