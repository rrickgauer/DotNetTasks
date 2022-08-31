using System.Data;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;

namespace Tasks.Mappers;

public static class RecurrenceMapper
{
    /// <summary>
    /// Map each DataRow in the table to a Recurrence domain model
    /// </summary>
    /// <param name="dataTable"></param>
    /// <returns></returns>
    public static List<Recurrence> ToModels(DataTable dataTable)
    {
        var recurrences =
            from dataRow
            in dataTable.AsEnumerable()
            select ToModel(dataRow);

        return recurrences.ToList();
    }

    /// <summary>
    /// Convert the data row into a recurrence domain model
    /// </summary>
    /// <param name="dataRow"></param>
    /// <returns></returns>
    public static Recurrence ToModel(DataRow dataRow)
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
        SqlCommandParmsMap parms = new();

        parms.Add("@user_id", recurrenceRetrieval.UserId);
        parms.Add("@range_start", recurrenceRetrieval.StartsOn);
        parms.Add("@range_end", recurrenceRetrieval.EndsOn);
        parms.Add("@return_results", true);

        return parms;
    }

    /// <summary>
    /// Get a dictionary for the get recurrences stored procedure
    /// </summary>
    /// <param name="recurrenceRetrieval"></param>
    /// <returns></returns>
    public static SqlCommandParmsMap ToSqlCommandParmsMap(EventRecurrenceRetrieval eventRecurrenceRetrieval)
    {
        SqlCommandParmsMap parms = new();

        parms.Add("@event_id", eventRecurrenceRetrieval.EventId);
        parms.Add("@user_id", eventRecurrenceRetrieval.UserId);
        parms.Add("@range_start", eventRecurrenceRetrieval.StartsOn);
        parms.Add("@range_end", eventRecurrenceRetrieval.EndsOn);
        parms.Add("@return_results", true);

        return parms;
    }
}
