using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Domain.Responses;
using Tasks.Repositories.Interfaces;
using Tasks.SQL.Commands;
using static Tasks.Domain.Responses.RepositoryResponses;

namespace Tasks.Repositories.Implementations;


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
