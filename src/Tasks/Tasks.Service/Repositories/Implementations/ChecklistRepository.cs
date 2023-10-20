using MySql.Data.MySqlClient;
using System.Data;
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

    public async Task<DataRow?> SelectChecklistByCommandLineReferenceAsync(uint commandLineReference)
    {
        MySqlCommand command = new(ChecklistCommands.SelectByCommandLineReference);

        command.Parameters.AddWithValue("@command_line_reference", commandLineReference);

        return await _dbConnection.FetchAsync(command);
    }

    /// <summary>
    /// Update or Insert the specified checklist.
    /// The command initially inserts the checklist, but updates it if it already exists.
    /// </summary>
    /// <param name="checklist"></param>
    /// <returns></returns>
    public async Task<int> SaveChecklistAsync(Checklist checklist)
    {
        MySqlCommand command = new(ChecklistCommands.Save);

        AddSaveChecklistParms(command, checklist);

        return await _dbConnection.ModifyAsync(command);
    }

    /// <summary>
    /// Add the appropriate checklist parm values to the given command.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="checklist"></param>
    private static void AddSaveChecklistParms(MySqlCommand command, Checklist checklist)
    {
        command.Parameters.AddWithValue("@id", checklist.Id);
        command.Parameters.AddWithValue("@user_id", checklist.UserId);
        command.Parameters.AddWithValue("@title", checklist.Title);
        command.Parameters.AddWithValue("@checklist_type_id", (ushort)checklist.ListType);
    }

    /// <summary>
    /// Execute a delete command.
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<int> DeleteChecklistAsync(Guid checklistId)
    {
        MySqlCommand command = new(ChecklistCommands.Delete);

        command.Parameters.AddWithValue("@id", checklistId);

        return await _dbConnection.ModifyAsync(command);
    }
}