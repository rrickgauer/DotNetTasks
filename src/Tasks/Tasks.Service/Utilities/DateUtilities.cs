using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Validation;

namespace Tasks.Service.Utilities;

public static class DateUtilities
{
    public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
    {
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }

    public static ValidDateRange GetDateRangeFromWeek(this DateTime date, DayOfWeek startingDay)
    {
        ValidDateRange range = new();

        range.StartsOn = date.StartOfWeek(startingDay);
        range.EndsOn = range.StartsOn.AddDays(7);

        return range;
    }

}
