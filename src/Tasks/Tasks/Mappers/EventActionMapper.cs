using Tasks.Domain.Models;

namespace Tasks.Mappers
{
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
    }
}
