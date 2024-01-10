using System.CommandLine.Binding;
using Tasks.Service.Domain.CliArgs.Cli.Label;

namespace Tasks.Cli.Binders.Label;

public class ListLabelArgsBinder : ArgsBinderBase<ListLabelArgs>, IValueDescriptor<ListLabelArgs>
{
    protected override ListLabelArgs GetBoundValue(BindingContext bindingContext)
    {
        return new();
    }
}
