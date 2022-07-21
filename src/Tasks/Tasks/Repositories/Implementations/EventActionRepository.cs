using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Mappers;
using Tasks.Repositories.Interfaces;

namespace Tasks.Repositories.Implementations
{
    public class EventActionRepository : IEventActionRepository
    {
        #region SQL Statements
        private sealed class SqlStatements
        {
            public const string MODIFY = @"
                REPLACE INTO Event_Actions 
                    (event_id, on_date, event_action_type_id, created_on)
                VALUES
                    (@event_id, @on_date, @event_action_type_id, @created_on)";

            public const string DELETE = @"
                DELETE FROM 
                    Event_Actions ea
                WHERE 
                    ea.event_id                 = @event_id
                    AND ea.on_date              = @on_date
                    AND ea.event_action_type_id = @event_action_type_id;";

            public const string SELECT = @"
                SELECT 
                    ea.event_id             AS event_id,
                    ea.on_date              AS on_date,
                    ea.event_action_type_id AS event_action_type_id,
                    ea.created_on           AS created_on
                FROM 
                    Event_Actions ea
                WHERE 
                    ea.event_id                 = @event_id
                    AND ea.on_date              = @on_date
                    AND ea.event_action_type_id = @event_action_type_id
                LIMIT 
                    1";
        }

        #endregion

        #region Private members
        private readonly IConfigs _configs;
        private readonly DbConnection _dbConnection;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configs"></param>
        public EventActionRepository(IConfigs configs)
        {
            _configs = configs;
            _dbConnection = new(_configs);
        }

        /// <summary>
        /// Save or insert the event
        /// </summary>
        /// <param name="eventAction"></param>
        /// <returns></returns>
        public int ModifyEventAction(EventAction eventAction)
        {
            // map the EventAction argument's values to the sql named params
            MySqlCommand command = new(SqlStatements.MODIFY);
            
            SqlCommandParmsMap parmsMap = EventActionMapper.ToSqlCommandParmsMap(eventAction);
            parmsMap.AddParmsToCommand(command);

            return _dbConnection.Modify(command);
        }

        /// <summary>
        /// Delete an event action
        /// </summary>
        /// <param name="eventAction"></param>
        /// <returns></returns>
        public int DeleteEventAction(EventAction eventAction)
        {
            // map the EventAction argument's values to the sql named params
            MySqlCommand command = new(SqlStatements.DELETE);

            SqlCommandParmsMap parmsMap = EventActionMapper.ToSqlCommandParmsMap(eventAction);
            parmsMap.Parms.Remove("@created_on");
            parmsMap.AddParmsToCommand(command);

            return _dbConnection.Modify(command);
        }

        public DataRow? GetEventAction(EventAction eventAction)
        {
            // map the EventAction argument's values to the sql named params
            MySqlCommand command = new(SqlStatements.SELECT);

            SqlCommandParmsMap parmsMap = EventActionMapper.ToSqlCommandParmsMap(eventAction);
            parmsMap.Parms.Remove("@created_on");
            parmsMap.AddParmsToCommand(command);

            return _dbConnection.Fetch(command);
        }
    }
}
