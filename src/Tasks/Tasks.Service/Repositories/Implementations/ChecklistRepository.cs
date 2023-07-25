using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Service.Configurations;
using Tasks.Service.Repositories.Commands;
using Tasks.Service.Repositories.Interfaces;

namespace Tasks.Service.Repositories.Implementations;

public class ChecklistRepository : IChecklistRepository
{

    #region Private members
    private readonly DbConnection _dbConnection;
    #endregion

    public ChecklistRepository(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<DataTable> SelectUserChecklistsAsync(Guid userId)
    {
        MySqlCommand command = new(ChecklistCommands.SelectAllByUser);

        command.Parameters.AddWithValue("@user_id", userId);

        var datatable = await _dbConnection.FetchAllAsync(command);

        return datatable;
    }
}