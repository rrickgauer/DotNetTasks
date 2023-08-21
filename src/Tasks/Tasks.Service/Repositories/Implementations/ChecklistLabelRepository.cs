using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Repositories.Commands;
using Tasks.Service.Repositories.Interfaces;

namespace Tasks.Service.Repositories.Implementations;

public class ChecklistLabelRepository : IChecklistLabelRepository
{
    #region - Private members -
    private readonly DbConnection _dbConnection;
    #endregion

    public ChecklistLabelRepository(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    /// <summary>
    /// Select all rows where the checklist_id matches the argument
    /// </summary>
    /// <param name="checklistId"></param>
    /// <returns></returns>
    public async Task<DataTable> SelectAllLabelsAssignedToChecklistAsync(Guid checklistId)
    {
        MySqlCommand command = new(ChecklistLabelCommands.SelectAllLabelsAssignedToChecklist);
        
        command.Parameters.AddWithValue("@checklist_id", checklistId);
        
        return await _dbConnection.FetchAllAsync(command);
    }

    /// <summary>
    /// Insert/Update a record
    /// </summary>
    /// <param name="checklistLabel"></param>
    /// <returns></returns>
    public async Task<int> ModifyAsync(ChecklistLabel checklistLabel)
    {
        MySqlCommand command = new(ChecklistLabelCommands.Replace);

        command.Parameters.AddWithValue("@checklist_id", checklistLabel.ChecklistId);
        command.Parameters.AddWithValue("@label_id", checklistLabel.LabelId);
        command.Parameters.AddWithValue("@created_on", checklistLabel.CreatedOn);

        return await _dbConnection.ModifyAsync(command);
    }

    /// <summary>
    /// Delete the matching checklist label record
    /// </summary>
    /// <param name="checklistLabel"></param>
    /// <returns></returns>
    public async Task<int> DeleteAsync(ChecklistLabelForm checklistLabel)
    {
        MySqlCommand command = new(ChecklistLabelCommands.Delete);

        command.Parameters.AddWithValue("@checklist_id", checklistLabel.ChecklistId);
        command.Parameters.AddWithValue("@label_id", checklistLabel.LabelId);

        return await _dbConnection.ModifyAsync(command);
    }
}
