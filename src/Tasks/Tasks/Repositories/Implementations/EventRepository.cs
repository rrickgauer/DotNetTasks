using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Mappers;
using Tasks.Repositories.Interfaces;
using Tasks.Security;

#pragma warning disable CS8602 // Dereference of a possibly null reference.   
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

namespace Tasks.Repositories.Implementations
{
    public class EventRepository : IEventRepository
    {
        #region Sql Statements
        private class SqlStatements
        {
            public const string SELECT_ALL = @"
                SELECT
                    *
                FROM
                    Events e
                WHERE
                    e.user_id = @userId";


            public const string DELETE = @"
                DELETE FROM
                    Events e
                WHERE
                    e.id = @id";

        }
        #endregion

        private readonly IConfigs _configs;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configs"></param>
        /// <param name="httpContextAccessor"></param>
        public EventRepository(IConfigs configs, IHttpContextAccessor httpContextAccessor)
        {
            _configs = configs;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get the specified event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Event? GetEvent(Guid eventId)
        {
            var userEvents = GetEvents();
            var filteredEvent = from result in userEvents where result.Id == eventId select result;

            Event? e = filteredEvent.Count() > 0 ? filteredEvent.First() : null;
            return e;
        }

        /// <summary>
        /// Get a list of all the user's events
        /// </summary>
        /// <returns></returns>
        public List<Event> GetEvents()
        {
            DbConnection conn = new(_configs);
            MySqlCommand cmd = BuildCommandForGetEvents();
            DataTable results = conn.FetchAll(cmd);

            List<Event> events = new();

            foreach(DataRow dr in results.Rows)
            {
                Event e = EventMapper.ToModel(dr);
                events.Add(e);
            }

            return events;
        }

        /// <summary>
        /// Build the MySqlCommand object for GetEvents
        /// </summary>
        /// <returns></returns>
        private MySqlCommand BuildCommandForGetEvents()
        {
            MySqlCommand cmd = new(SqlStatements.SELECT_ALL);

            var userId = GetCurrentUserId();
            cmd.Parameters.Add(new("@userId", userId));

            return cmd;
        }

        /// <summary>
        /// Delete the specified event 
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool DeleteEvent(Guid eventId)
        {
            MySqlCommand command = new(SqlStatements.DELETE);
            command.Parameters.Add(new("@id", eventId));

            DbConnection connection = new(_configs);
            int numRowsAffected = connection.Modify(command);

            return numRowsAffected > 0;
        }

        /// <summary>
        /// Get the current user id
        /// </summary>
        /// <returns></returns>
        private Guid? GetCurrentUserId()
        {
            return SecurityMethods.GetUserIdFromRequest(_httpContextAccessor.HttpContext.Request);
        }


    }
}
