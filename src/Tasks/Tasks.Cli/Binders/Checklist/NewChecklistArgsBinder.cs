using System.CommandLine;
using System.CommandLine.Binding;
using Tasks.Cli.CommandArgs.Groups;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.CliArgs.Cli.Checklist;

namespace Tasks.Cli.Binders.Checklist;

public class NewChecklistArgsBinder : ArgsBinderBase<NewChecklistArgs>, IValueDescriptor<NewChecklistArgs>
{
    [CopyTo(nameof(NewChecklistArgs.Title))]
    public Option<string?> TitleOption { get; set; } = ChecklistCommandGroup.TitleOption;
}
