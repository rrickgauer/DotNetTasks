using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Binding;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Domain.CliArgs.Cli.Checklist;

namespace Tasks.Cli.Binders.Checklist;

public class EditChecklistArgsBinder : ArgsBinderBase<EditChecklistArgs>, IValueDescriptor<EditChecklistArgs>
{
    public Option<string?> TitleOption { get; set; }
    public Argument<int?> ChecklistReferenceArgument { get; set; }

    public EditChecklistArgsBinder(Option<string?> titleOption, Argument<int?> checklistReferenceArgument)
    {
        TitleOption = titleOption;
        ChecklistReferenceArgument = checklistReferenceArgument;
    }

    protected override EditChecklistArgs GetBoundValue(BindingContext bindingContext)
    {
        return new()
        {
            ChecklistReference = bindingContext.ParseResult.GetValueForArgument(ChecklistReferenceArgument),
            Title = bindingContext.ParseResult.GetValueForOption(TitleOption),
        };
    }


}
