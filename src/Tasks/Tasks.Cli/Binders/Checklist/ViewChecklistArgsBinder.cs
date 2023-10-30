using System.CommandLine;
using System.CommandLine.Binding;
using Tasks.Cli.CommandArgs.Groups;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.CliArgs.Cli.Checklist;

namespace Tasks.Cli.Binders.Checklist;

public class ViewChecklistArgsBinder : ArgsBinderBase<ViewChecklistArgs>, IValueDescriptor<ViewChecklistArgs>
{

    [CopyTo(nameof(ViewChecklistArgs.CommandLineId), ParseFunctionName = nameof(BinderCasts.ParseUInt))]
    public Argument<uint?> IndexArgument { get; set; } = ChecklistCommandGroup.IndexArgument;

    //public ViewChecklistArgsBinder(Argument<uint?> indexArgument)
    //{
    //    IndexArgument = indexArgument;
    //}

    //protected override ViewChecklistArgs GetBoundValue(BindingContext bindingContext)
    //{
    //    return new()
    //    {
    //        CommandLineId = bindingContext.ParseResult.GetValueForArgument(IndexArgument),
    //    };
    //}
}
