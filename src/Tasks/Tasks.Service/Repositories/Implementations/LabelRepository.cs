using Tasks.Service.Domain.Models;
using Tasks.Service.Repositories.Interfaces;
using MySql.Data.MySqlClient;
using Tasks.Service.Repositories.Commands;
using System.Data;

namespace Tasks.Service.Repositories.Implementations;


public class LabelRepository : ILabelRepository
{
    #region Private memebers
    private readonly DbConnection _dbConnection;
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configs"></param>
    public LabelRepository(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }



    /// <summary>
    /// Select all label records for the specified user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<DataTable> SelectLabelsAsync(Guid userId)
    {
        MySqlCommand command = new(LabelCommands.SelectAllByUserId);

        command.Parameters.AddWithValue("@user_id", userId);

        return await _dbConnection.FetchAllAsync(command);
    }

    public async Task<int> ModifyLabelAsync(Label label)
    {
        MySqlCommand command = new(LabelCommands.Modify);

        command.Parameters.AddWithValue("@id", label.Id);
        command.Parameters.AddWithValue("@user_id", label.UserId);
        command.Parameters.AddWithValue("@name", label.Name);
        command.Parameters.AddWithValue("@color", label.Color);
        command.Parameters.AddWithValue("@created_on", label.CreatedOn);

        return await _dbConnection.ModifyAsync(command);
    }


    /// <summary>
    /// Get the a the label data row that has the given id
    /// </summary>
    /// <param name="labelId"></param>
    /// <returns></returns>
    public async Task<DataRow?> SelectLabelAsync(Guid labelId)
    {
        MySqlCommand command = new(LabelCommands.SelectById);

        command.Parameters.AddWithValue("@id", labelId);

        return await _dbConnection.FetchAsync(command);
    }


    public async Task<int> DeleteLabelAsync(Guid labelId)
    {
        MySqlCommand command = new(LabelCommands.Delete);

        command.Parameters.AddWithValue("@id", labelId);

        return await _dbConnection.ModifyAsync(command);
    }
}
