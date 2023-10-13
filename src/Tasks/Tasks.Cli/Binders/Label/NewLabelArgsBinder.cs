using System.CommandLine;
using System.CommandLine.Binding;
using Tasks.Service.Domain.CliArgs.Cli.Label;

namespace Tasks.Cli.Binders.Label;

public class NewLabelArgsBinder : ArgsBinderBase<NewLabelArgs>, IValueDescriptor<NewLabelArgs>
{
    public Option<string?> NameOption { get; set; }
    public Option<string?> ColorOption { get; set; }

    public NewLabelArgsBinder(Option<string?> nameOption, Option<string?> colorOption)
    {
        NameOption = nameOption;
        ColorOption = colorOption;
    }

    protected override NewLabelArgs GetBoundValue(BindingContext bindingContext)
    {
        return new()
        {
            Name = bindingContext.ParseResult.GetValueForOption(NameOption),
            Color = bindingContext.ParseResult.GetValueForOption(ColorOption),
        };
    }
}
