using System.CommandLine;
using System.CommandLine.Binding;
using Tasks.Service.Domain.CliArgs.Cli.Label;

namespace Tasks.Cli.Binders.Label;

public class EditLabelArgsBinder : ArgsBinderBase<EditLabelArgs>, IValueDescriptor<EditLabelArgs>
{
    public Argument<uint?> IndexArgument { get; set; }
    public Option<string?> NameOption { get; set; }
    public Option<string?> ColorOption { get; set; }

    public EditLabelArgsBinder(Argument<uint?> indexArgument, Option<string?> nameOption, Option<string?> colorOption)
    {
        IndexArgument = indexArgument;
        NameOption = nameOption;
        ColorOption = colorOption;
    }

    protected override EditLabelArgs GetBoundValue(BindingContext bindingContext)
    {
        return new()
        {
            Name = bindingContext.ParseResult.GetValueForOption(NameOption),
            Color = bindingContext.ParseResult.GetValueForOption(ColorOption),
            Index = bindingContext.ParseResult.GetValueForArgument(IndexArgument),
        };
    }
}
