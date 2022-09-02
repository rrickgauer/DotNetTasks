using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Configurations;
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
    public async Task<SelectAllResponse> SelectLabels(Guid userId)
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
}
