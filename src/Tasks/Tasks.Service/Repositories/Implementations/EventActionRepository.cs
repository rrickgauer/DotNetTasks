using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Service.Configurations;
using Tasks.Service.Domain.Models;

using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Repositories.Commands;
using Tasks.Service.Mappers;

namespace Tasks.Service.Repositories.Implementations;

public class EventActionRepository : IEventActionRepository
{
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

    
    public async Task<int> ModifyEventActionAsync(EventAction eventAction)
    {
        // map the EventAction argument's values to the sql named params
        MySqlCommand command = new(EventActionRepositorySql.Modify);

        SqlCommandParmsMap parmsMap = EventActionMapper.ToSqlCommandParmsMap(eventAction);
        parmsMap.AddParmsToCommand(command);

        var result =  await _dbConnection.ModifyAsync(command);
        return result;
    }

    public async Task<int> DeleteEventActionAsync(EventAction eventAction)
    {
        // map the EventAction argument's values to the sql named params
        MySqlCommand command = new(EventActionRepositorySql.Delete);

        SqlCommandParmsMap parmsMap = EventActionMapper.ToSqlCommandParmsMap(eventAction);
        parmsMap.Parms.Remove("@created_on");
        parmsMap.AddParmsToCommand(command);

        var result = await _dbConnection.ModifyAsync(command);
        return result;
    }

    public async Task<DataRow?> GetEventActionAsync(EventAction eventAction)
    {
        // map the EventAction argument's values to the sql named params
        MySqlCommand command = new(EventActionRepositorySql.Select);

        SqlCommandParmsMap parmsMap = EventActionMapper.ToSqlCommandParmsMap(eventAction);
        parmsMap.Parms.Remove("@created_on");
        parmsMap.AddParmsToCommand(command);

        var result = await _dbConnection.FetchAsync(command);
        return result;
    }
}
