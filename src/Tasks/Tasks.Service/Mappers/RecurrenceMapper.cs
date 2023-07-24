using System.Data;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Mappers.Interfaces;

namespace Tasks.Service.Mappers;

public class RecurrenceMapper : ModelMapper<Recurrence>
{
    /// <summary>
    /// Convert the data row into a recurrence domain model
    /// </summary>
    /// <param name="dataRow"></param>
    /// <returns></returns>
    public override Recurrence ToModel(DataRow dataRow)
    {
        Recurrence recurrence = new()
        {
            EventId   = dataRow.Field<Guid?>("event_id"),
            Completed = dataRow.Field<bool?>("completed"),
            Cancelled = dataRow.Field<bool?>("cancelled"),
            Name      = dataRow.Field<string?>("name"),
            UserId    = dataRow.Field<Guid?>("user_id"),
            OccursOn  = dataRow.Field<DateTime?>("occurs_on"),
            StartsAt  = dataRow.Field<TimeSpan?>("starts_at"),
        };

        return recurrence;
    }

    /// <summary>
    /// Get a dictionary for the get recurrences stored procedure
    /// </summary>
    /// <param name="recurrenceRetrieval"></param>
    /// <returns></returns>
    public static SqlCommandParmsMap ToSqlCommandParmsMap(RecurrenceRetrieval recurrenceRetrieval)
    {
        SqlCommandParmsMap parms = new()
        {
            { "@user_id", recurrenceRetrieval.UserId },
            { "@range_start", recurrenceRetrieval.StartsOn },
            { "@range_end", recurrenceRetrieval.EndsOn },
            { "@return_results", true }
        };

        return parms;
    }

    /// <summary>
    /// Get a dictionary for the get recurrences stored procedure
    /// </summary>
    /// <param name="recurrenceRetrieval"></param>
    /// <returns></returns>
    public static SqlCommandParmsMap ToSqlCommandParmsMap(RecurrenceRetrieval eventRecurrenceRetrieval, Guid eventId)
    {
        SqlCommandParmsMap parms = new()
        {
            { "@event_id", eventId },
            { "@user_id", eventRecurrenceRetrieval.UserId },
            { "@range_start", eventRecurrenceRetrieval.StartsOn },
            { "@range_end", eventRecurrenceRetrieval.EndsOn },
            { "@return_results", true }
        };

        return parms;
    }
}
