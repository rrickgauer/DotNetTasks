using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Service.Configurations;
using Tasks.Service.Domain.Models;

using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Repositories.Commands;
using Tasks.Service.Mappers;

namespace Tasks.Service.Repositories.Implementations;

public partial class EventRepository : IEventRepository
{
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

    /// <summary>
    /// Get the specified user's events
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
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
        MySqlCommand cmd = new(EventRepositorySql.SelectAllUsersEvents);

        //var userId = GetCurrentUserId();
        cmd.Parameters.Add(new("@userId", userId));

        return cmd;
    }

    /// <summary>
    /// Delete the specified event from the database
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    public async Task<int> DeleteEventAsync(Guid eventId)
    {
        MySqlCommand command = new(EventRepositorySql.Delete);
        command.Parameters.Add(new("@id", eventId));

        DbConnection connection = new(_configs);

        var result = await connection.ModifyAsync(command);

        return result;
    }

    /// <summary>
    /// Modify the specified event (insert/update)
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
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
        MySqlCommand command = new(EventRepositorySql.ModifyEventProcedure)
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
        
        MySqlCommand command = new(EventRepositorySql.SelectById);
        command.Parameters.AddWithValue("@id", eventId);

        var result = await connection.FetchAsync(command);

        return result;
    }

}
