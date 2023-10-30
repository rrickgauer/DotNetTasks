using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Utilities;

namespace Tasks.Cli.Binders;

public static class BinderCasts
{

    public static object? ParseUInt(object? value)
    {
        if (value == null)
            return null;

        string textValue = value?.ToString();

        if (uint.TryParse(textValue, out var result))
        {
            return result;
        }

        return null;
    }


    public static object? ParseCliChecklistItemStatus(object? value)
    {
        var enumItems = EnumUtilities.GetEnumEntries<CliChecklistItemStatus>();

        foreach (var enumItem in enumItems)
        {
            var name = Enum.GetName(enumItem);

            if (name == value?.ToString())
            {
                return enumItem;    
            }
        }

        return null;

    }


}
