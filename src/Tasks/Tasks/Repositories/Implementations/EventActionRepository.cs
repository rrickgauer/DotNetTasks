using MySql.Data.MySqlClient;
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
            MySqlCommand cmd = new(SqlStatements.MODIFY);
            
            SqlCommandParmsMap map = EventActionMapper.ToSqlCommandParmsMap(eventAction);
            map.AddParmsToCommand(cmd);

            return _dbConnection.Modify(cmd);
        }

        /// <summary>
        /// Delete an event action
        /// </summary>
        /// <param name="eventAction"></param>
        /// <returns></returns>
        public int DeleteEventAction(EventAction eventAction)
        {
            // map the EventAction argument's values to the sql named params
            MySqlCommand cmd = new(SqlStatements.DELETE);

            SqlCommandParmsMap map = EventActionMapper.ToSqlCommandParmsMap(eventAction);
            map.Parms.Remove("@created_on");
            map.AddParmsToCommand(cmd);

            return _dbConnection.Modify(cmd);
        }
    }
}
