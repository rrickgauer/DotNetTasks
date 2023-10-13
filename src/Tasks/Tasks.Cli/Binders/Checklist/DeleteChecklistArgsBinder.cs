

using System.CommandLine;
using System.CommandLine.Binding;
using Tasks.Service.Domain.CliArgs.Cli.Checklist;

namespace Tasks.Cli.Binders.Checklist;

public class DeleteChecklistArgsBinder : ArgsBinderBase<DeleteChecklistArgs>, IValueDescriptor<DeleteChecklistArgs> 
{
    public Option<bool> ForceOption { get; set; }
    public Argument<int?> ChecklistReferenceArgument { get; set; }

    public DeleteChecklistArgsBinder(Option<bool> forceOption, Argument<int?> checklistReferenceArgument)
    {
        ForceOption = forceOption;
        ChecklistReferenceArgument = checklistReferenceArgument;
    }

    protected override DeleteChecklistArgs GetBoundValue(BindingContext bindingContext)
    {
        return new()
        {
            Index = bindingContext.ParseResult.GetValueForArgument(ChecklistReferenceArgument),
            Force = bindingContext.ParseResult.GetValueForOption(ForceOption),
        };
    }
}
