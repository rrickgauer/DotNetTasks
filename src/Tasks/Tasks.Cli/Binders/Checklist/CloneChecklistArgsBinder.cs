using System.CommandLine;
using System.CommandLine.Binding;
using Tasks.Service.Domain.CliArgs.Cli.Checklist;

namespace Tasks.Cli.Binders.Checklist;

public class CloneChecklistArgsBinder : ArgsBinderBase<CloneChecklistArgs>, IValueDescriptor<CloneChecklistArgs> 
{
    public Option<string?> TitleOption { get; set; }
    public Argument<int?> ChecklistReferenceArgument { get; set; }

    public CloneChecklistArgsBinder(Option<string?> titleOption, Argument<int?> checklistReferenceArgument)
    {
        TitleOption = titleOption;
        ChecklistReferenceArgument = checklistReferenceArgument;
    }

    protected override CloneChecklistArgs GetBoundValue(BindingContext bindingContext)
    {
        return new()
        {
            Index = bindingContext.ParseResult.GetValueForArgument(ChecklistReferenceArgument),
            Title = bindingContext.ParseResult.GetValueForOption(TitleOption),
        };
    }

}
