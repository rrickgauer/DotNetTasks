using System.Data;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;
using Tasks.Service.Mappers.Interfaces;

namespace Tasks.Service.Mappers;

public class EventMapper : ModelMapper<Event>
{

    /// <summary>
    /// Transform the given datarow into an Event domain model
    /// </summary>
    /// <param name="dataRow"></param>
    /// <returns></returns>
    public override Event ToModel(DataRow dataRow)
    {
        Event newEvent = new()
        {
            Id              = dataRow.Field<Guid?>("id"),
            UserId          = dataRow.Field<Guid?>("user_id"),
            Name            = dataRow.Field<string?>("name"),
            Description     = dataRow.Field<string?>("description"),
            PhoneNumber     = dataRow.Field<string?>("phone_number"),
            Location        = dataRow.Field<string?>("location"),
            StartsOn        = dataRow.Field<DateTime?>("starts_on"),
            EndsOn          = dataRow.Field<DateTime?>("ends_on"),
            StartsAt        = dataRow.Field<TimeSpan?>("starts_at"),
            EndsAt          = dataRow.Field<TimeSpan?>("ends_at"),
            Frequency       = dataRow.Field<Frequency?>("frequency"),
            Separation      = dataRow.Field<uint?>("separation"),
            CreatedOn       = dataRow.Field<DateTime?>("created_on"),
            RecurrenceDay   = dataRow.Field<int?>("recurrence_day"),
            RecurrenceWeek  = dataRow.Field<int?>("recurrence_week"),
            RecurrenceMonth = dataRow.Field<int?>("recurrence_month"),
        };

        return newEvent;
    }

    /// <summary>
    /// Map the specified event to a dictionary whose keys are mapped to the Modify_Event sql stored procedure
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public static SqlCommandParmsMap ToSqlCommandParmsMap(Event e)
    {
        SqlCommandParmsMap parms = new()
        {
            { "@in_id", e.Id },
            { "@in_user_id", e.UserId },
            { "@in_name", e.Name },
            { "@in_description", e.Description },
            { "@in_phone_number", e.PhoneNumber },
            { "@in_location", e.Location },
            { "@in_starts_on", e.StartsOn },
            { "@in_ends_on", e.EndsOn },
            { "@in_starts_at", e.StartsAt },
            { "@in_ends_at", e.EndsAt },
            { "@in_frequency", e.Frequency },
            { "@in_separation", e.Separation },
            { "@in_recurrence_day", e.RecurrenceDay },
            { "@in_recurrence_week", e.RecurrenceWeek },
            { "@in_recurrence_month", e.RecurrenceMonth }
        };

        return parms;
    }

}
