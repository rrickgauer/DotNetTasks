using Spectre.Console;
using System.Collections;
using System.Reflection;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Utilities;

namespace Tasks.Service.Domain.Contracts;

public interface ICliTable
{
    public static abstract void AddTableColumns(Table table);
    
    public Hashtable ToDict()
    {
        Hashtable result = new();

        var props = AttributeUtilities.GetPropertiesWithAttributeDict<CliTableColumnAttribute>(GetType());

        foreach (var prop in props)
        {
            string key = prop.Attribute.Header;
            var value = prop.GetValueRaw(this);

            result.Add(key, value);
        }

        return result;
    }


    public List<string> GetTableRow()
    {
        List<string> columns = new();

        var properties = AttributeUtilities.GetPropertiesWithAttribute<CliTableColumnAttribute>(GetType());

        foreach (var prop in properties)
        {
            if (IsPropertyValueNull(prop, out var valueText))
            {
                columns.Add(valueText);
                continue;
            }

            string? result = valueText;

            if (IsCustomMethodGiven(prop, out MethodInfo? customMethod))
            {
                result = customMethod?.Invoke(this, null)?.ToString();
            }

            columns.Add(result ?? string.Empty);
        }

        return columns;
    }


    private bool IsPropertyValueNull(PropertyInfo prop, out string valueText)
    {
        valueText = prop.GetValue(this)?.ToString() ?? string.Empty;

        return string.IsNullOrEmpty(valueText);
    }

    private bool IsCustomMethodGiven(PropertyInfo prop, out MethodInfo? methodInfo)
    {
        var stringMethodName = prop.GetCustomAttribute<CliTableColumnAttribute>()?.ToStringMethodName;

        methodInfo = string.IsNullOrEmpty(stringMethodName) ? null : GetType().GetMethod(stringMethodName);        

        return methodInfo != null;
    }



}
