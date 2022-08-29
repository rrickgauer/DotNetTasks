using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Repositories.Interfaces;

namespace Tasks.Repositories.Implementations
{
    public class UserEmailVerificationRepository : IUserEmailVerificationRepository
    {
        #region SQL Statements
        private sealed class SqlStatements
        {
            public const string MODIFY = @"
               INSERT INTO
                    User_Email_Verifications (id, user_id, email, confirmed_on, created_on)
                VALUES
                    (@id, @user_id, @email, @confirmed_on, @created_on) AS new_values ON DUPLICATE KEY
                UPDATE
                    confirmed_on = new_values.confirmed_on";
        }

        #endregion

        #region Private members
        private readonly IConfigs _configs;
        private readonly DbConnection _dbConnection;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configs"></param>
        public UserEmailVerificationRepository(IConfigs configs)
        {
            _configs = configs;
            _dbConnection = new(_configs);
        }

        /// <summary>
        /// Insert a new UserEmailVerification record
        /// </summary>
        /// <param name="userEmailVerification"></param>
        /// <returns>Number of records affected by the sql command</returns>
        public async Task<int> InsertAsync(UserEmailVerification userEmailVerification)
        {
            int numRecords = await ModifyAsync(userEmailVerification);

            return numRecords;
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
            MySqlCommand command = new(SqlStatements.MODIFY);

            command.Parameters.AddWithValue("@id", userEmailVerification.Id);
            command.Parameters.AddWithValue("@user_id", userEmailVerification.UserId);
            command.Parameters.AddWithValue("@email", userEmailVerification.Email);
            command.Parameters.AddWithValue("@confirmed_on", userEmailVerification.ConfirmedOn);
            command.Parameters.AddWithValue("@created_on", userEmailVerification.CreatedOn);

            return command;
        }
    }
}
