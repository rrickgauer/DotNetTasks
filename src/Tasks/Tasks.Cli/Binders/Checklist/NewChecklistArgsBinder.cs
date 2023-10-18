using System.CommandLine;
using System.CommandLine.Binding;
using Tasks.Cli.CommandArgs;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.CliArgs.Cli.Checklist;

namespace Tasks.Cli.Binders.Checklist;

public class NewChecklistArgsBinder : ArgsBinderBase<NewChecklistArgs>, IValueDescriptor<NewChecklistArgs>
{
    [CopyTo(nameof(NewChecklistArgs.Title))]
    public Option<string?> TitleOption { get; set; } = ChecklistCommandGroup.TitleOption;

    //public NewChecklistArgsBinder(Option<string?> titleOption)
    //{
    //    TitleOption = titleOption;
    //}

    //protected override NewChecklistArgs GetBoundValue(BindingContext bindingContext)
    //{
    //    return new()
    //    {
    //        Title = bindingContext.ParseResult.GetValueForOption(TitleOption),
    //    };
    //}
}
