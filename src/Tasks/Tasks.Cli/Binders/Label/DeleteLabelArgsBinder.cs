using System.CommandLine;
using System.CommandLine.Binding;
using Tasks.Cli.CommandArgs.Groups;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.CliArgs.Cli.Label;

namespace Tasks.Cli.Binders.Label;

public class DeleteLabelArgsBinder : ArgsBinderBase<DeleteLabelArgs>, IValueDescriptor<DeleteLabelArgs>
{
    [CopyTo(nameof(DeleteLabelArgs.Index))]
    public Option<uint?> ChecklistReferenceArgument { get; set; } = LabelCommandGroup.IndexOption;

    [CopyTo(nameof(DeleteLabelArgs.Force))]
    public Option<bool> ForceOption { get; set; } = LabelCommandGroup.ForceOption;
}
