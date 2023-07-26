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

    public async Task<DataTable> SelectChecklistItemsAsync(Guid checklistId)
    {
        MySqlCommand command = new(ChecklistItemCommands.SelectAllChecklistItems);

        command.Parameters.AddWithValue("@checklist_id", checklistId);

        return await _connection.FetchAllAsync(command);
    }


    public async Task<DataRow?> SelectChecklistItemAsync(Guid itemId)
    {
        MySqlCommand command = new(ChecklistItemCommands.SelectSingle);

        command.Parameters.AddWithValue("@id", itemId);

        return await _connection.FetchAsync(command);
    }


    public async Task<int> DeleteChecklistItemAsync(Guid itemId)
    {
        throw new NotImplementedException();
    }

    public async Task<int> SaveChecklistItemAsync(ChecklistItem checklistItem)
    {
        throw new NotImplementedException();
    }
}
