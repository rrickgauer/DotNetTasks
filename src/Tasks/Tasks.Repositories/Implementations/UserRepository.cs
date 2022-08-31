using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Repositories.Interfaces;
using Tasks.SQL.Commands;

namespace Tasks.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    #region Private members
    private readonly IConfigs _configs;
    private readonly DbConnection _dbConnection;
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configs"></param>
    public UserRepository(IConfigs configs)
    {
        _configs = configs;
        _dbConnection = new DbConnection(_configs);
    }

    #region Get user

    /// <summary>
    /// Get the user from the email/password combination.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<DataRow?> GetUserAsync(string email, string password)
    {
        // setup a new sql command
        MySqlCommand cmd = new(UserRepositorySql.SelectFromEmailPassword);

        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@password", password);

        return await _dbConnection.FetchAsync(cmd);
    }

    /// <summary>
    /// Get the user from the datbase that has the given email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<DataRow?> GetUserAsync(string email)
    {
        // setup a new sql command
        MySqlCommand cmd = new(UserRepositorySql.SelectFromEmail);
        
        cmd.Parameters.AddWithValue("@email", email);

        return await _dbConnection.FetchAsync(cmd);
    }


    /// <summary>
    /// Get a user from the database by their id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<DataRow?> GetUserAsync(Guid userId)
    {
        // setup a new sql command
        MySqlCommand cmd = new(UserRepositorySql.SelectFromId);

        cmd.Parameters.AddWithValue("@id", userId);

        return await _dbConnection.FetchAsync(cmd);
    }

    #endregion

    /// <summary>
    /// Update the user password
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<int> UpdateUserPasswordAsync(Guid userId, string password)
    {
        MySqlCommand cmd = new(UserRepositorySql.UpdatePassword);

        cmd.Parameters.AddWithValue("@password", password);
        cmd.Parameters.AddWithValue("@id", userId);

        int numRecords = await _dbConnection.ModifyAsync(cmd);

        return numRecords;
    }


    /// <summary>
    /// Insert the user into the database
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<int> InsertUserAsync(User user)
    {
        MySqlCommand command = new(UserRepositorySql.Modify);

        command.Parameters.AddWithValue("@id", user.Id);
        command.Parameters.AddWithValue("@email", user.Email);
        command.Parameters.AddWithValue("@password", user.Password);
        command.Parameters.AddWithValue("@created_on", user.CreatedOn);

        int numRecords = await _dbConnection.ModifyAsync(command);

        return numRecords;
    }


    public async Task<DataRow?> GetUserViewAsync(Guid userId)
    {
        MySqlCommand command = new(UserRepositorySql.SelectFromView);

        command.Parameters.AddWithValue("@id", userId);

        return await _dbConnection.FetchAsync(command);
    }
}
