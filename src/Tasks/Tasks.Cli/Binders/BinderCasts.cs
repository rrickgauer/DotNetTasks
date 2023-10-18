using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    

}
