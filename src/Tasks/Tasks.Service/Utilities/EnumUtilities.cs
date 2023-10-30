using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Domain.Enums;

namespace Tasks.Service.Utilities;

public static class EnumUtilities
{
    public static IEnumerable<TEnum> GetEnumEntries<TEnum>() where TEnum : Enum
    {
        return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
    }

    

    
}
