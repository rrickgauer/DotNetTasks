using System.Data;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;

namespace Tasks.Mappers
{
    public static class RecurrenceMapper
    {
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
                Name      = dataRow.Field<string?>("name"),
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
        public static Dictionary<string, object?> ToStoredProcDictionary(RecurrenceRetrieval recurrenceRetrieval)
        {
            Dictionary<string, object?> parms = new();

            parms.Add("@user_id", recurrenceRetrieval.UserId);
            parms.Add("@range_start", recurrenceRetrieval.StartsOn);
            parms.Add("@range_end", recurrenceRetrieval.EndsOn);

            return parms;
        }

        /// <summary>
        /// Get a dictionary for the get recurrences stored procedure
        /// </summary>
        /// <param name="recurrenceRetrieval"></param>
        /// <returns></returns>
        public static Dictionary<string, object?> ToStoredProcDictionary(EventRecurrenceRetrieval eventRecurrenceRetrieval)
        {
            Dictionary<string, object?> parms = new();

            parms.Add("@event_id", eventRecurrenceRetrieval.EventId);
            parms.Add("@user_id", eventRecurrenceRetrieval.UserId);
            parms.Add("@range_start", eventRecurrenceRetrieval.StartsOn);
            parms.Add("@range_end", eventRecurrenceRetrieval.EndsOn);
            parms.Add("@return_results", true);

            return parms;
        }
    }
}
