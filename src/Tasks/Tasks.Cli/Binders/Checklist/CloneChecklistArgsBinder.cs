using System.CommandLine;
using System.CommandLine.Binding;
using Tasks.Cli.CommandArgs.Groups;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.CliArgs.Cli.Checklist;

namespace Tasks.Cli.Binders.Checklist;

public class CloneChecklistArgsBinder : ArgsBinderBase<CloneChecklistArgs>, IValueDescriptor<CloneChecklistArgs> 
{
    [CopyTo(nameof(CloneChecklistArgs.Title))]
    public Option<string?> TitleOption { get; set; } = ChecklistCommandGroup.TitleOption;

    [CopyTo(nameof(CloneChecklistArgs.CommandLineId), ParseFunctionName = nameof(BinderCasts.ParseUInt))]
    public Argument<uint?> ChecklistReferenceArgument { get; set; } = ChecklistCommandGroup.IndexArgument;
}
