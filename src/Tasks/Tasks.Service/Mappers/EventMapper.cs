using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using Tasks.Service.CustomAttributes;
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
        SqlCommandParmsMap parms = new();

        parms.Add("@in_id", e.Id);
        parms.Add("@in_user_id", e.UserId);
        parms.Add("@in_name", e.Name);
        parms.Add("@in_description", e.Description);
        parms.Add("@in_phone_number", e.PhoneNumber);
        parms.Add("@in_location", e.Location);
        parms.Add("@in_starts_on", e.StartsOn);
        parms.Add("@in_ends_on", e.EndsOn);
        parms.Add("@in_starts_at", e.StartsAt);
        parms.Add("@in_ends_at", e.EndsAt);
        parms.Add("@in_frequency", e.Frequency);
        parms.Add("@in_separation", e.Separation);
        parms.Add("@in_recurrence_day", e.RecurrenceDay);
        parms.Add("@in_recurrence_week", e.RecurrenceWeek);
        parms.Add("@in_recurrence_month", e.RecurrenceMonth);

        return parms;
    }

}
