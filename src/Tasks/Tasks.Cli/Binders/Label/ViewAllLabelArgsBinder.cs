using System.CommandLine.Binding;
using Tasks.Service.Domain.CliArgs.Cli.Label;

namespace Tasks.Cli.Binders.Label;

public class ViewAllLabelArgsBinder : ArgsBinderBase<ViewAllLabelArgs>, IValueDescriptor<ViewAllLabelArgs>
{
    protected override ViewAllLabelArgs GetBoundValue(BindingContext bindingContext)
    {
        return new();
    }
}
