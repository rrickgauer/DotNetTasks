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
            public const string SELECT_ALL_USERS_EVENTS = @"
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

            public const string SELECT_BY_ID = @"
                SELECT 
                    * 
                FROM 
                    Events e 
                WHERE
                    e.id = @id
                LIMIT 1";

            public const string MODIFY_EVENT_PROCEDURE = "Modify_Event";

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
        /// Get the specified event from the user's events
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Event? GetUserEvent(Guid eventId)
        {
            var userEvents = GetUserEvents();
            var filteredEvent = from result in userEvents where result.Id == eventId select result;

            Event? e = filteredEvent.Count() > 0 ? filteredEvent.First() : null;
            return e;
        }

        /// <summary>
        /// Get a list of all the user's events
        /// </summary>
        /// <returns></returns>
        public List<Event> GetUserEvents()
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
            MySqlCommand cmd = new(SqlStatements.SELECT_ALL_USERS_EVENTS);

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
        /// Modify the specified event
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool ModifyEvent(Event e)
        {
            // make a new connection
            DbConnection connection = new(_configs);

            // create a new sql command loaded with all the parms from the event argument
            MySqlCommand cmd = SetupModifyEventMySqlCommand(e);

            // execute the query
            int numRowsAffected = connection.Modify(cmd);

            return numRowsAffected >= 0;
        }

        /// <summary>
        /// setup a new stored procedure command for the Modify event method
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private MySqlCommand SetupModifyEventMySqlCommand(Event e)
        {
            // setup a new stored procedure command 
            MySqlCommand command = new(SqlStatements.MODIFY_EVENT_PROCEDURE)
            {
                CommandType = CommandType.StoredProcedure,
            };

            // add all the named parms to the command
            foreach (var parm in EventMapper.ToStoredProcDictionary(e))
            {
                command.Parameters.AddWithValue(parm.Key, parm.Value);
            }

            return command;
        }d


        /// <summary>
        /// Get the specified event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Event? GetEvent(Guid eventId)
        {
            DbConnection conn = new(_configs);
            MySqlCommand cmd = new(SqlStatements.SELECT_BY_ID);
            cmd.Parameters.AddWithValue("@id", eventId);

            DataRow? row = conn.Fetch(cmd);

            Event? theEvent = row != null ? EventMapper.ToModel(row) : null;
            
            return theEvent;
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
