using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Service.Configurations;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Repositories.Helpers;
using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Repositories.Commands;

namespace Tasks.Service.Repositories.Implementations;

public class EventLabelRepository : IEventLabelRepository
{
    #region Private members
    private readonly DbConnection _dbConnection;
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configs"></param>
    public EventLabelRepository(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    /// <summary>
    /// Insert the EventLabel object into the database
    /// </summary>
    /// <param name="eventLabel"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<int> InsertAsync(EventLabel eventLabel)
    {
        MySqlCommand command = new(EventLabelCommands.Insert);

        command.Parameters.AddWithValue("@event_id", eventLabel.EventId);
        command.Parameters.AddWithValue("@label_id", eventLabel.LabelId);
        command.Parameters.AddWithValue("@created_on", eventLabel.CreatedOn);

        return await _dbConnection.ModifyAsync(command);
    }

    /// <summary>
    /// Get all event label assignments for the specified event
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<DataTable> SelectAllForEventAsync(Guid eventId)
    {
        MySqlCommand command = new(EventLabelCommands.SelectAllByIdAndUserId);

        command.Parameters.AddWithValue("@event_id", eventId);

        DataTable records = await _dbConnection.FetchAllAsync(command);

        return records;        
    }

    /// <summary>
    /// Insert multiple event label assignments
    /// </summary>
    /// <param name="eventLabelsBatchRequest"></param>
    /// <returns></returns>
    public async Task<int> InsertBatchAsync(EventLabelsBatchRequest eventLabelsBatchRequest)
    {
        MySqlCommand deleteCommand = new(EventLabelCommands.DeleteAllByEvent);
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

    /// <summary>
    /// Get all event label assignments from the database for the specified user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<DataTable> SelectAllAsync(Guid userId)
    {
        MySqlCommand command = new(EventLabelCommands.SelectAllByUser);

        command.Parameters.AddWithValue("@user_id", userId);

        DataTable records = await _dbConnection.FetchAllAsync(command);

        return records;
    }


    /// <summary>
    /// Delete the event label assignment record
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="labelId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<int> DeleteAsync(Guid eventId, Guid labelId)
    {
        MySqlCommand command = new(EventLabelCommands.Delete);

        command.Parameters.AddWithValue("@event_id", eventId);
        command.Parameters.AddWithValue("@label_id", labelId);

        int numRecords = await _dbConnection.ModifyAsync(command);

        return numRecords;
    }
}
