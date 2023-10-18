using System.CommandLine;
using System.CommandLine.Binding;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.CliArgs.Cli.Checklist;

namespace Tasks.Cli.Binders.Checklist;

public class CloneChecklistArgsBinder : ArgsBinderBase<CloneChecklistArgs>, IValueDescriptor<CloneChecklistArgs> 
{
    public Option<string?> TitleOption { get; set; }

    [CopyTo(nameof(CloneChecklistArgs.CommandLineId))]
    public Argument<uint?> ChecklistReferenceArgument { get; set; }

    public CloneChecklistArgsBinder(Option<string?> titleOption, Argument<uint?> checklistReferenceArgument)
    {
        TitleOption = titleOption;
        ChecklistReferenceArgument = checklistReferenceArgument;
    }

    protected override CloneChecklistArgs GetBoundValue(BindingContext bindingContext)
    {
        return new()
        {
            CommandLineId = bindingContext.ParseResult.GetValueForArgument(ChecklistReferenceArgument),
            Title = bindingContext.ParseResult.GetValueForOption(TitleOption),
        };
    }

}
