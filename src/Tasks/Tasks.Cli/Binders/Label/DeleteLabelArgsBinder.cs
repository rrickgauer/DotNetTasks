using System.CommandLine;
using System.CommandLine.Binding;
using Tasks.Service.Domain.CliArgs.Cli.Label;

namespace Tasks.Cli.Binders.Label;

public class DeleteLabelArgsBinder : ArgsBinderBase<DeleteLabelArgs>, IValueDescriptor<DeleteLabelArgs>
{
    public Argument<int?> ChecklistReferenceArgument { get; set; }
    public Option<bool> ForceOption { get; set; }

    public DeleteLabelArgsBinder(Argument<int?> checklistReferenceArgument, Option<bool> forceOption)
    {
        ChecklistReferenceArgument = checklistReferenceArgument;
        ForceOption = forceOption;
    }

    protected override DeleteLabelArgs GetBoundValue(BindingContext bindingContext)
    {
        return new()
        {
            Index = bindingContext.ParseResult.GetValueForArgument(ChecklistReferenceArgument),
            Force = bindingContext.ParseResult.GetValueForOption(ForceOption),
        };
    }
}
