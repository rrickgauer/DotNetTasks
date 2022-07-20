using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Configurations;
using Tasks.Domain.Parms;
using Tasks.Mappers;
using Tasks.Repositories.Interfaces;

namespace Tasks.Repositories.Implementations
{
    public class RecurrenceRepository : IRecurrenceRepository
    {
        #region Sql statements
        private class SqlStatements
        {
            public const string GET_RECURRENCES = "Get_Recurrences";
            public const string GET_EVENT_RECURRENCES = "Get_Event_Recurrences";
        }
        #endregion


        #region Private memebers
        private readonly IConfigs _configs;
        private readonly DbConnection _dbConnection;
        #endregion

        public RecurrenceRepository(IConfigs configs)
        {
            _configs = configs;
            _dbConnection = new(configs);
        }

        public DataTable GetRecurrences(RecurrenceRetrieval recurrenceRetrieval)
        {
            // setup a new stored procedure command 
            MySqlCommand command = new(SqlStatements.GET_RECURRENCES)
            {
                CommandType = CommandType.StoredProcedure,
            };

            // add all the named parms to the command
            foreach (var parm in RecurrenceMapper.ToStoredProcDictionary(recurrenceRetrieval))
            {
                command.Parameters.AddWithValue(parm.Key, parm.Value);
            }

            return _dbConnection.FetchAll(command);
        }

        public DataTable GetEventRecurrences(EventRecurrenceRetrieval eventRecurrenceRetrieval)
        {
            // setup a new stored procedure command 
            MySqlCommand command = new(SqlStatements.GET_EVENT_RECURRENCES)
            {
                CommandType = CommandType.StoredProcedure,
            };

            // add all the named parms to the command
            foreach (var parm in RecurrenceMapper.ToStoredProcDictionary(eventRecurrenceRetrieval))
            {
                command.Parameters.AddWithValue(parm.Key, parm.Value);
            }

            return _dbConnection.FetchAll(command);
        }


    }
}
