using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Repositories.Helpers;
using Tasks.Repositories.Interfaces;
using Tasks.SQL.Commands;

namespace Tasks.Repositories.Implementations;

public class EventLabelRepository : IEventLabelRepository
{
    #region Private members
    private readonly IConfigs _configs;
    private readonly DbConnection _dbConnection;
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configs"></param>
    public EventLabelRepository(IConfigs configs)
    {
        _configs = configs;
        _dbConnection = new(_configs);
    }

    /// <summary>
    /// Insert the EventLabel object into the database
    /// </summary>
    /// <param name="eventLabel"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<int> InsertAsync(EventLabel eventLabel, Guid userId)
    {
        MySqlCommand command = new(EventLabelRepositorySql.Insert);

        command.Parameters.AddWithValue("@event_id", eventLabel.EventId);
        command.Parameters.AddWithValue("@label_id", eventLabel.LabelId);
        command.Parameters.AddWithValue("@created_on", eventLabel.CreatedOn);
        command.Parameters.AddWithValue("@user_id", userId);

        int numRecords = await _dbConnection.ModifyAsync(command);

        return numRecords;
    }

    public async Task<DataTable> SelectAllAsync(Guid eventId, Guid userId)
    {
        MySqlCommand command = new(EventLabelRepositorySql.SelectAllByIdAndUserId);

        command.Parameters.AddWithValue("@event_id", eventId);
        command.Parameters.AddWithValue("@user_id", userId);

        DataTable records = await _dbConnection.FetchAllAsync(command);

        return records;        
    }

    public async Task<int> InsertBatchAsync(EventLabelsBatchRequest eventLabelsBatchRequest)
    {
        MySqlCommand deleteCommand = new(EventLabelRepositorySql.DeleteAllByEvent);
        deleteCommand.Parameters.AddWithValue("@event_id", eventLabelsBatchRequest.EventId);

        // don't need to run the insert command because there are no labels to add
        // so just delete all the existing ones
        if (eventLabelsBatchRequest.LabelIds.ToList().Count == 0)
        {
            return await _dbConnection.ModifyAsync(deleteCommand);
        }

        EventLabelsBatchInsertSqlGenerator commandGenerator = new(eventLabelsBatchRequest);
        MySqlCommand insertCommand = commandGenerator.GetMySqlCommand();

        var result = await _dbConnection.ModifyWithTransactionAsync(deleteCommand, insertCommand);

        return 0;
    }

    public async Task<DataTable> SelectAllAsync(Guid userId)
    {
        MySqlCommand command = new(EventLabelRepositorySql.SelectAllByUser);

        command.Parameters.AddWithValue("@user_id", userId);

        DataTable records = await _dbConnection.FetchAllAsync(command);

        return records;
    }
}
