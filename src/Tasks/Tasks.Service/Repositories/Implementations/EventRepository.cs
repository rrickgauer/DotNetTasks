using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Service.Domain.Models;
using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Repositories.Commands;
using Tasks.Service.Mappers;

namespace Tasks.Service.Repositories.Implementations;

public partial class EventRepository : IEventRepository
{
    private readonly DbConnection _dbConnection;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configs"></param>
    /// <param name="httpContextAccessor"></param>
    public EventRepository(DbConnection connection)
    {
        _dbConnection = connection;
    }

    /// <summary>
    /// Get the specified user's events
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<DataTable> GetUserEventsAsync(Guid userId)
    {   
        MySqlCommand command = BuildCommandForGetUserEvents(userId);
        var results = await _dbConnection.FetchAllAsync(command);

        return results;
    }

    /// <summary>
    /// Build the MySqlCommand object for GetEvents
    /// </summary>
    /// <returns></returns>
    private MySqlCommand BuildCommandForGetUserEvents(Guid userId)
    {
        MySqlCommand command = new(EventCommands.SelectAllUsersEvents);

        command.Parameters.Add(new("@userId", userId));

        return command;
    }

    /// <summary>
    /// Delete the specified event from the database
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    public async Task<int> DeleteEventAsync(Guid eventId)
    {
        MySqlCommand command = new(EventCommands.Delete);

        command.Parameters.Add(new("@id", eventId));

        var result = await _dbConnection.ModifyAsync(command);

        return result;
    }

    /// <summary>
    /// Modify the specified event (insert/update)
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public async Task<int> ModifyEventAsync(Event e)
    {
        // create a new sql command loaded with all the parms from the event argument
        MySqlCommand command = SetupModifyEventMySqlCommand(e);

        // execute the query
        var result = await _dbConnection.ModifyAsync(command);

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
        MySqlCommand command = new(EventCommands.ModifyEventProcedure)
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
        MySqlCommand command = new(EventCommands.SelectById);
        
        command.Parameters.AddWithValue("@id", eventId);

        var result = await _dbConnection.FetchAsync(command);

        return result;
    }

}
