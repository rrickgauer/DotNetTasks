using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Configurations;
using Tasks.Domain.Parms;
using Tasks.Mappers;
using Tasks.Repositories.Interfaces;
using Tasks.SQL.Commands;

namespace Tasks.Repositories.Implementations;

public class RecurrenceRepository : IRecurrenceRepository
{

    #region Private memebers
    private readonly IConfigs _configs;
    private readonly DbConnection _dbConnection;
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configs"></param>
    public RecurrenceRepository(IConfigs configs)
    {
        _configs = configs;
        _dbConnection = new(configs);
    }


    /// <summary>
    /// Get the recurrences from the database
    /// </summary>
    /// <param name="recurrenceRetrieval"></param>
    /// <returns></returns>
    public async Task<DataTable> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval)
    {
        // setup a new stored procedure command 
        MySqlCommand command = new(RecurrenceRepositorySql.GetRecurrences)
        {
            CommandType = CommandType.StoredProcedure,
        };

        var map = RecurrenceMapper.ToSqlCommandParmsMap(recurrenceRetrieval);
        map.AddParmsToCommand(command);

        var result = await _dbConnection.FetchAllAsync(command);
        return result;
    }

    /// <summary>
    /// Get the recurrences for a single event from the database
    /// </summary>
    /// <param name="eventRecurrenceRetrieval"></param>
    /// <returns></returns>
    public async Task<DataTable> GetEventRecurrencesAsync(EventRecurrenceRetrieval eventRecurrenceRetrieval)
    {
        // setup a new stored procedure command 
        MySqlCommand command = new(RecurrenceRepositorySql.GetEventRecurrences)
        {
            CommandType = CommandType.StoredProcedure,
        };

        var map = RecurrenceMapper.ToSqlCommandParmsMap(eventRecurrenceRetrieval);
        map.AddParmsToCommand(command);

        var result = await _dbConnection.FetchAllAsync(command);
        return result;
    }
}
