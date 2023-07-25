using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Configurations;
using Tasks.Service.Domain.Models;
using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Repositories.Commands;

namespace Tasks.Service.Repositories.Implementations;

public class UserEmailVerificationRepository : IUserEmailVerificationRepository
{

    #region Private members
    private readonly DbConnection _dbConnection;
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configs"></param>
    public UserEmailVerificationRepository(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    /// <summary>
    /// Insert a new UserEmailVerification record
    /// </summary>
    /// <param name="userEmailVerification"></param>
    /// <returns>Number of records affected by the sql command</returns>
    public async Task<int> InsertAsync(UserEmailVerification userEmailVerification)
    {
        return await ModifyAsync(userEmailVerification);
    }

    /// <summary>
    /// Update the speicified UserEmailVerification object in the database
    /// </summary>
    /// <param name="userEmailVerification"></param>
    /// <returns></returns>
    public async Task<int> UpdateAsync(UserEmailVerification userEmailVerification)
    {
        return await ModifyAsync(userEmailVerification);
    }

    /// <summary>
    /// Execute the modify sql command on the specified UserEmailVerification.
    /// This either inserts a new record or updates an existing one.
    /// </summary>
    /// <param name="userEmailVerification"></param>
    /// <returns></returns>
    private async Task<int> ModifyAsync(UserEmailVerification userEmailVerification)
    {
        MySqlCommand cmd = GetModifySqlCommand(userEmailVerification);

        int numRecords = await _dbConnection.ModifyAsync(cmd);

        return numRecords;
    }

    /// <summary>
    /// Build a new MySqlCommand for the modify query with the specified UserEmailVerification object values.
    /// </summary>
    /// <param name="userEmailVerification"></param>
    /// <returns></returns>
    private static MySqlCommand GetModifySqlCommand(UserEmailVerification userEmailVerification)
    {
        MySqlCommand command = new(UserEmailVerificationCommands.Modify);

        command.Parameters.AddWithValue("@id", userEmailVerification.Id);
        command.Parameters.AddWithValue("@user_id", userEmailVerification.UserId);
        command.Parameters.AddWithValue("@email", userEmailVerification.Email);
        command.Parameters.AddWithValue("@confirmed_on", userEmailVerification.ConfirmedOn);
        command.Parameters.AddWithValue("@created_on", userEmailVerification.CreatedOn);

        return command;
    }

    /// <summary>
    /// Get the specified data row
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DataRow?> GetAsync(Guid id)
    {
        MySqlCommand command = new(UserEmailVerificationCommands.Select);

        command.Parameters.AddWithValue("@id", id);

        return await _dbConnection.FetchAsync(command);
    }
}
