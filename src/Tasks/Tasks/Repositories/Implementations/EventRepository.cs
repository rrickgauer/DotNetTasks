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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configs"></param>
        /// <param name="httpContextAccessor"></param>
        public EventRepository(IConfigs configs)
        {
            _configs = configs;
        }


        public async Task<DataTable> GetUserEventsAsync(Guid userId)
        {
            DbConnection connection = new(_configs);
            MySqlCommand command = BuildCommandForGetUserEvents(userId);
            var results = await connection.FetchAllAsync(command);

            return results;
        }



        /// <summary>
        /// Build the MySqlCommand object for GetEvents
        /// </summary>
        /// <returns></returns>
        private MySqlCommand BuildCommandForGetUserEvents(Guid userId)
        {
            MySqlCommand cmd = new(SqlStatements.SELECT_ALL_USERS_EVENTS);

            //var userId = GetCurrentUserId();
            cmd.Parameters.Add(new("@userId", userId));

            return cmd;
        }


        public async Task<int> DeleteEventAsync(Guid eventId)
        {
            MySqlCommand command = new(SqlStatements.DELETE);
            command.Parameters.Add(new("@id", eventId));

            DbConnection connection = new(_configs);

            var result = await connection.ModifyAsync(command);

            return result;
        }


        public async Task<int> ModifyEventAsync(Event e)
        {
            // make a new connection
            DbConnection connection = new(_configs);

            // create a new sql command loaded with all the parms from the event argument
            MySqlCommand cmd = SetupModifyEventMySqlCommand(e);

            // execute the query
            var result = await connection.ModifyAsync(cmd);

            return result;
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
            var map = EventMapper.ToSqlCommandParmsMap(e);
            map.AddParmsToCommand(command);

            return command;
        }

        /// <summary>
        /// Get the specified event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public async Task<DataRow?> GetEventAsync(Guid eventId)
        {
            DbConnection connection = new(_configs);
            
            MySqlCommand command = new(SqlStatements.SELECT_BY_ID);
            command.Parameters.AddWithValue("@id", eventId);

            var result = await connection.FetchAsync(command);

            return result;
        }




    }
}
