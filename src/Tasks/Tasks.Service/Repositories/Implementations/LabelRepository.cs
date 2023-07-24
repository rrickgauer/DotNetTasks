using Tasks.Service.Configurations;
using Tasks.Service.Domain.Models;
using Tasks.Service.Repositories.Interfaces;
using static Tasks.Service.Domain.Responses.RepositoryResponses;
using MySql.Data.MySqlClient;
using Tasks.Service.Repositories.Commands;

namespace Tasks.Service.Repositories.Implementations;


public class LabelRepository : ILabelRepository
{
    #region Private memebers
    private readonly IConfigs _configs;
    private readonly DbConnection _dbConnection;
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configs"></param>
    public LabelRepository(IConfigs configs)
    {
        _configs = configs;
        _dbConnection = new(_configs);
    }

    /// <summary>
    /// Get all the labels for a user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<SelectAllResponse> SelectLabelsAsync(Guid userId)
    {
        SelectAllResponse result = new()
        {
            Successful = true,
        };

        MySqlCommand command = new(LabelRepositorySql.SelectAllByUserId);

        command.Parameters.AddWithValue("@user_id", userId);

        result.Data = await _dbConnection.FetchAllAsync(command);

        return result;
    }


    public async Task<ModifyResponse> ModifyLabelAsync(Label label)
    {
        ModifyResponse result = new()
        {
            Successful = true,
        };

        MySqlCommand command = new(LabelRepositorySql.Modify);

        command.Parameters.AddWithValue("@id", label.Id);
        command.Parameters.AddWithValue("@user_id", label.UserId);
        command.Parameters.AddWithValue("@name", label.Name);
        command.Parameters.AddWithValue("@color", label.Color);
        command.Parameters.AddWithValue("@created_on", label.CreatedOn);

        result.Data = await _dbConnection.ModifyAsync(command);

        return result;
    }

    /// <summary>
    /// Get the a the label data row that has the given id
    /// </summary>
    /// <param name="labelId"></param>
    /// <returns></returns>
    public async Task<SelectResponse> SelectLabelAsync(Guid labelId)
    {
        SelectResponse result = new()
        {
            Successful = true,
        };

        MySqlCommand command = new(LabelRepositorySql.SelectById);

        command.Parameters.AddWithValue("@id", labelId);

        result.Data = await _dbConnection.FetchAsync(command);

        return result;

    }

    public async Task<ModifyResponse> DeleteLabelAsync(Label label)
    {
        ModifyResponse result = new()
        {
            Successful = true,
        };

        MySqlCommand command = new(LabelRepositorySql.DeleteByIdAndUserId);

        command.Parameters.AddWithValue("@id", label.Id);
        command.Parameters.AddWithValue("@user_id", label.UserId);

        result.Data = await _dbConnection.ModifyAsync(command);

        return result;
    }
}
