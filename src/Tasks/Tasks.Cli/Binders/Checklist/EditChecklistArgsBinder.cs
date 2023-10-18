using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Binding;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Cli.CommandArgs;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.CliArgs.Cli.Checklist;

namespace Tasks.Cli.Binders.Checklist;

public class EditChecklistArgsBinder : ArgsBinderBase<EditChecklistArgs>, IValueDescriptor<EditChecklistArgs>
{
    [CopyTo(nameof(EditChecklistArgs.Title))]
    public Option<string?> TitleOption { get; set; } = ChecklistCommandGroup.TitleOption;

    [CopyTo(nameof(EditChecklistArgs.CommandLineId), ParseFunctionName = nameof(BinderCasts.ParseUInt))]
    public Argument<uint?> ChecklistReferenceArgument { get; set; } = ChecklistCommandGroup.IndexArgument;
}
