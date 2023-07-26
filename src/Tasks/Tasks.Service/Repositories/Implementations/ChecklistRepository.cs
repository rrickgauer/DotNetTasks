using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Service.Configurations;
using Tasks.Service.Domain.Models;
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

    /// <summary>
    /// Select all the user's checklist records from View_Checklists
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<DataTable> SelectUserChecklistsAsync(Guid userId)
    {
        MySqlCommand command = new(ChecklistCommands.SelectAllByUser);

        command.Parameters.AddWithValue("@user_id", userId);

        var datatable = await _dbConnection.FetchAllAsync(command);

        return datatable;
    }

    /// <summary>
    /// Select a single checklist
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    public async Task<DataRow?> SelectChecklistAsync(Guid checklistId)
    {
        MySqlCommand command = new(ChecklistCommands.SelectSingle);

        command.Parameters.AddWithValue("@checklist_id", checklistId);

        return await _dbConnection.FetchAsync(command);
    }



}