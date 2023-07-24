using System.Data;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;

namespace Tasks.Service.Mappers;

public static class EventActionMapper
{
    /// <summary>
    /// Get a dictionary for the get recurrences stored procedure
    /// </summary>
    /// <param name="recurrenceRetrieval"></param>
    /// <returns></returns>
    public static SqlCommandParmsMap ToSqlCommandParmsMap(EventAction recurrenceRetrieval)
    {
        SqlCommandParmsMap map = new();

        map.Add("@event_id", recurrenceRetrieval.EventId);
        map.Add("@on_date", recurrenceRetrieval.OnDate);
        map.Add("@event_action_type_id", recurrenceRetrieval.EventActionType);
        map.Add("@created_on", recurrenceRetrieval.CreatedOn);

        return map;
    }

    /// <summary>
    /// Map the specified DataRow to an EventAction domain model
    /// </summary>
    /// <param name="dataRow"></param>
    /// <returns></returns>
    public static EventAction ToModel(DataRow dataRow)
    {
        EventAction eventAction = new()
        {
            EventId         = dataRow.Field<Guid?>("event_id"),
            OnDate          = dataRow.Field<DateTime?>("on_date"),
            EventActionType = dataRow.Field<EventActionType?>("event_action_type_id"),
            CreatedOn       = dataRow.Field<DateTime?>("created_on"),
        };

        return eventAction;
    }
}
