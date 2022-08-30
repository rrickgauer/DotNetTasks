using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Mappers;
using Tasks.Repositories.Interfaces;

namespace Tasks.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        #region Sql Statements
        private sealed class SqlStatements
        {
            public const string SELECT_FROM_EMAIL_PASSWORD = @"  
                SELECT 
                    u.id AS id,
                    u.email AS email,
                    u.password AS password,
                    u.created_on AS created_on
                FROM
                    Users u
                WHERE 
                    u.email = @email
                    AND u.password = @password
                LIMIT 1";


            public const string SELECT_FROM_EMAIL = @"
                SELECT 
                    u.id AS id,
                    u.email AS email,
                    u.password AS password,
                    u.created_on AS created_on
                FROM
                    Users u
                WHERE 
                    u.email = @email
                LIMIT 1";


            public const string SELECT_FROM_ID = @"
                SELECT 
                    *
                FROM
                    View_Users u
                WHERE 
                    u.id = @id
                LIMIT 1";


            public const string UPDATE_PASSWORD = @"
                UPDATE
                    Users
                SET
                    password = @password
                WHERE
                    id = @id";

            public const string MODIFY = @"
                INSERT INTO
                    Users (id, email, password, created_on)
                VALUES
                    (@id, @email, @password, @created_on) AS new_values ON DUPLICATE KEY
                UPDATE
                    email = new_values.email,
                    password = new_values.password";


            public const string SELECT_FROM_VIEW = @"
                SELECT
                    *
                FROM
                    View_Users u
                WHERE
                    u.id = @id
                LIMIT
                    1";
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
            MySqlCommand cmd = new(SqlStatements.SELECT_FROM_EMAIL_PASSWORD);

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
            MySqlCommand cmd = new(SqlStatements.SELECT_FROM_EMAIL);
            
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
            MySqlCommand cmd = new(SqlStatements.SELECT_FROM_ID);

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
            MySqlCommand cmd = new(SqlStatements.UPDATE_PASSWORD);

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
            MySqlCommand command = new(SqlStatements.MODIFY);

            command.Parameters.AddWithValue("@id", user.Id);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@created_on", user.CreatedOn);

            int numRecords = await _dbConnection.ModifyAsync(command);

            return numRecords;
        }


        public async Task<DataRow?> GetUserViewAsync(Guid userId)
        {
            MySqlCommand command = new(SqlStatements.SELECT_FROM_VIEW);

            command.Parameters.AddWithValue("@id", userId);

            return await _dbConnection.FetchAsync(command);
        }
    }
}
