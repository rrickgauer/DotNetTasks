using CommandLine;
using System.CommandLine;
using System.CommandLine.Binding;
using System.CommandLine.Parsing;
using System.Reflection;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.CliArgs.Cli.Checklist;

namespace Tasks.Cli.Binders.Checklist;

public class CloneChecklistArgsBinder : ArgsBinderBase<CloneChecklistArgs>, IValueDescriptor<CloneChecklistArgs> 
{
    public Option<string?> TitleOption { get; set; }

    [CopyTo(nameof(CloneChecklistArgs.Index))]
    //public Argument<int?> ChecklistReferenceArgument { get; set; }
    public Argument ChecklistReferenceArgument { get; set; }

    public CloneChecklistArgsBinder(Option<string?> titleOption, Argument<int?> checklistReferenceArgument)
    {
        TitleOption = titleOption;
        ChecklistReferenceArgument = checklistReferenceArgument;
    }

    protected override CloneChecklistArgs GetBoundValue(BindingContext bindingContext)
    {




        var strongTypeArgResult = bindingContext.ParseResult.GetValueForArgument(ChecklistReferenceArgument);


        var strongType = strongTypeArgResult?.GetType();

        CloneChecklistArgs result = new();

        var prop = typeof(CloneChecklistArgsBinder).GetProperty(nameof(ChecklistReferenceArgument));

        var propType = prop.PropertyType;

        


        var resultPropertyName = prop?.GetCustomAttribute<CopyToAttribute>()?.Name;

        if (prop?.GetValue(this) is Argument argument)
        {
            Type outputType = Nullable.GetUnderlyingType(argument.ValueType) ?? argument.ValueType;

            var argumentValue = bindingContext.ParseResult.GetValueForArgument(argument);

            if (argumentValue is Token token)
            {
                var newone = Convert.ChangeType(token.Value, outputType);

                //Convert.

                int x = 10;
            }

            

            var val2 = bindingContext.ParseResult.FindResultFor(argument);

            


            var weakType = argumentValue?.GetType();

            result?.GetType()?.GetProperty(resultPropertyName ?? string.Empty)?.SetValue(result, argumentValue);
        }





        return new()
        {
            Index = bindingContext.ParseResult.GetValueForArgument(ChecklistReferenceArgument) as int?,
            Title = bindingContext.ParseResult.GetValueForOption(TitleOption),
        };
    }

}
