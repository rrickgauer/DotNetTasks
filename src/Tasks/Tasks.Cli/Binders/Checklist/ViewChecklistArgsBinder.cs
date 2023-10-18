using System.CommandLine;
using System.CommandLine.Binding;
using Tasks.Service.Domain.CliArgs.Cli.Checklist;

namespace Tasks.Cli.Binders.Checklist;

public class ViewChecklistArgsBinder : ArgsBinderBase<ViewChecklistArgs>, IValueDescriptor<ViewChecklistArgs>
{
    public Argument<uint?> IndexArgument { get; set; }

    public ViewChecklistArgsBinder(Argument<uint?> indexArgument)
    {
        IndexArgument = indexArgument;
    }

    protected override ViewChecklistArgs GetBoundValue(BindingContext bindingContext)
    {
        return new()
        {
            CommandLineId = bindingContext.ParseResult.GetValueForArgument(IndexArgument),
        };
    }
}
