using System.CommandLine;
using System.CommandLine.Binding;
using System.CommandLine.Parsing;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.Other;
using Tasks.Service.Utilities;

namespace Tasks.Cli.Binders;

public class ArgsBinderBase<T> : BinderBase<T> where T : new()
{

    protected override T GetBoundValue(BindingContext bindingContext)
    {
        T result = new();

        var propertiesWithAttribute = AttributeUtilities.GetPropertiesWithAttributeDict<CopyToAttribute>(GetType());

        foreach (var customAttributeProperty in propertiesWithAttribute)
        {
            var parsedValue = GetParsedCliArgValue(customAttributeProperty, bindingContext.ParseResult);

            if (parsedValue != null)
            {
                var resultProperty = typeof(T).GetProperty(customAttributeProperty.Attribute.Name);
                resultProperty?.SetValue(result, parsedValue);
            }
        }

        return result;
    }

    protected virtual object? GetParsedCliArgValue(CustomAttributeProperty<CopyToAttribute> customAttributeProperty, ParseResult parseResult)
    {
        var cliArg = customAttributeProperty.GetValueRaw(this);

        if (cliArg == null)
        {
            return null;
        }
        else if (cliArg is Option parsedOption)
        {
            return parseResult.GetValueForOption(parsedOption);
        }
        else if (cliArg is Argument parsedArgument)
        {
            return GetArgumentValue(parseResult, parsedArgument, customAttributeProperty.Attribute);
        }
        else
        {
            throw new NotSupportedException($"{cliArg?.GetType()} is not supported");
        }
    }

    protected virtual object? GetArgumentValue(ParseResult parseResult, Argument parsedArgument, CopyToAttribute copyToAttribute)
    {
        //var parsedValueString = parseResult.GetValueForArgument(parsedArgument)?.ToString();
        var valueRaw = parseResult.GetValueForArgument(parsedArgument)?.ToString();

        if (!copyToAttribute.HasParseFunction)
        {
            return valueRaw;
        }

        var callback = typeof(BinderCasts).GetMethod(copyToAttribute.ParseFunctionName);
        var parsedValue = callback?.Invoke(null, new[] { valueRaw });

        return parsedValue;

    }
}
