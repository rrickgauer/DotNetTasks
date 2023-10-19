using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Service.Domain.Models;
using Tasks.Service.Repositories.Commands;
using Tasks.Service.Repositories.Interfaces;

namespace Tasks.Service.Repositories.Implementations;

public class ChecklistItemRepository : IChecklistItemRepository
{

    #region - Private Members -

    private readonly DbConnection _connection;

    #endregion


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="connection"></param>
    public ChecklistItemRepository(DbConnection connection)
    {
        _connection = connection;
    }

    /// <summary>
    /// Select all items within the specified checklist
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    public async Task<DataTable> SelectChecklistItemsAsync(Guid checklistId)
    {
        MySqlCommand command = new(ChecklistItemCommands.SelectAllChecklistItems);

        command.Parameters.AddWithValue("@checklist_id", checklistId);

        return await _connection.FetchAllAsync(command);
    }

    /// <summary>
    /// Select a single checklist item
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public async Task<DataRow?> SelectChecklistItemAsync(Guid itemId)
    {
        MySqlCommand command = new(ChecklistItemCommands.SelectSingle);

        command.Parameters.AddWithValue("@id", itemId);

        return await _connection.FetchAsync(command);
    }

    
    /// <summary>
    /// Delete the specified checklist
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<int> DeleteChecklistItemAsync(Guid itemId)
    {
        MySqlCommand command = new(ChecklistItemCommands.Delete);

        command.Parameters.AddWithValue("@id", itemId);

        return await _connection.ModifyAsync(command);
    }


    /// <summary>
    /// Save the specified checklist
    /// </summary>
    /// <param name="checklistItem"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<int> SaveChecklistItemAsync(ChecklistItem checklistItem)
    {
        MySqlCommand command = new(ChecklistItemCommands.Modify);

        command.Parameters.AddWithValue("@id", checklistItem.Id);
        command.Parameters.AddWithValue("@checklist_id", checklistItem.ChecklistId);
        command.Parameters.AddWithValue("@content", checklistItem.Content);
        command.Parameters.AddWithValue("@position", checklistItem.Position);
        command.Parameters.AddWithValue("@completed_on", checklistItem.CompletedOn);

        return await _connection.ModifyAsync(command);
    }


    /// <summary>
    /// Insert copies of every checklist item beloning to the sourceChecklistId into the target checklist.
    /// </summary>
    /// <param name="sourceChecklistId">Source checklist</param>
    /// <param name="targetChecklistId">Target checklist</param>
    /// <returns>Number of rows affected</returns>
    public async Task<int> CopyChecklistItemsAsync(Guid sourceChecklistId, Guid targetChecklistId)
    {
        MySqlCommand command = new(ChecklistItemCommands.CopyItems);

        command.Parameters.AddWithValue("@source_checklist_id", sourceChecklistId);
        command.Parameters.AddWithValue("@target_checklist_id", targetChecklistId);

        return await _connection.ModifyAsync(command);
    }

    public async Task<DataTable> SelectChecklistItemViewsAsync(Guid checklistId)
    {
        MySqlCommand command = new(ChecklistItemCommands.SelectAllViewChecklistItems);

        command.Parameters.AddWithValue("@checklist_id", checklistId);

        return await _connection.FetchAllAsync(command);
    }

    public async Task<DataTable> SelectChecklistItemViewsByCliIdAsync(uint checklistCommandLineReferenceId)
    {
        MySqlCommand command = new(ChecklistItemCommands.SelectAllByChecklistCliReference);

        command.Parameters.AddWithValue("@checklist_command_line_reference", checklistCommandLineReferenceId);

        return await _connection.FetchAllAsync(command);
    }
}
