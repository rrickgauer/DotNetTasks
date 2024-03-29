﻿using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Service.Configurations;
using Tasks.Service.Domain.Parms;

using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Repositories.Commands;
using Tasks.Service.Mappers;
using Tasks.Service.Validation;

namespace Tasks.Service.Repositories.Implementations;

public class RecurrenceRepository : IRecurrenceRepository
{

    #region Private memebers
    private readonly DbConnection _dbConnection;
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configs"></param>
    public RecurrenceRepository(IConfigs configs, DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }


    /// <summary>
    /// Get the recurrences from the database
    /// </summary>
    /// <param name="recurrenceRetrieval"></param>
    /// <returns></returns>
    public async Task<DataTable> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval)
    {
        // setup a new stored procedure command 
        MySqlCommand command = new(RecurrenceCommands.GetRecurrences)
        {
            CommandType = CommandType.StoredProcedure,
        };

        var map = RecurrenceMapper.ToSqlCommandParmsMap(recurrenceRetrieval);
        map.AddParmsToCommand(command);

        var result = await _dbConnection.FetchAllAsync(command);
        return result;
    }

    /// <summary>
    /// Get the recurrences for the specified event
    /// </summary>
    /// <param name="recurrenceRetrieval"></param>
    /// <param name="eventId"></param>
    /// <returns></returns>
    public async Task<DataTable> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval, Guid eventId)
    {
        // setup a new stored procedure command 
        MySqlCommand command = new(RecurrenceCommands.GetEventRecurrences)
        {
            CommandType = CommandType.StoredProcedure,
        };

        var map = RecurrenceMapper.ToSqlCommandParmsMap(recurrenceRetrieval, eventId);
        map.AddParmsToCommand(command);

        var result = await _dbConnection.FetchAllAsync(command);

        return result;
    }


    public async Task<DataTable> GetRecurrencesForRemindersAsync(IValidDateRange validDateRange)
    {
        // setup a new stored procedure command 
        MySqlCommand command = new(RecurrenceCommands.GetRecurrencesForReminders)
        {
            CommandType = CommandType.StoredProcedure,
        };

        // add the parms
        command.Parameters.AddWithValue("@range_start", validDateRange.StartsOn);
        command.Parameters.AddWithValue("@range_end", validDateRange.EndsOn);

        // call the stored proc
        DataTable dataTable = await _dbConnection.FetchAllAsync(command);
        
        return dataTable;
    }


}
