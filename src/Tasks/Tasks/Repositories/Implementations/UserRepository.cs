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
        private class SqlStatements
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
        }
        #endregion

        private readonly IConfigs _configs;
        private readonly DbConnection _dbConnection;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configs"></param>
        public UserRepository(IConfigs configs)
        {
            _configs = configs;
            _dbConnection = new DbConnection(_configs);
        }   

        /// <summary>
        /// Get the user from the email/password combination.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User?> GetUserAsync(string email, string password)
        {
            // setup a new sql command
            MySqlCommand cmd = GetMySqlCommandForGetUserWithEmailPassword(email, password);

            // fetch the record from the database
            DataRow? record = await _dbConnection.FetchAsync(cmd);

            // map the user to the record's values if the datarow is not null
            User? user = record != null ? UserMapper.ToModel(record) : null;

            return user;
        }



        /// <summary>
        /// Get a MySqlCommand for the GetUser method.
        /// Adds the named parms to the command.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private MySqlCommand GetMySqlCommandForGetUserWithEmailPassword(string email, string password)
        {
            MySqlCommand cmd = new(SqlStatements.SELECT_FROM_EMAIL_PASSWORD);

            cmd.Parameters.Add(new("@email", email));
            cmd.Parameters.Add(new("@password", password));

            return cmd;
        }


    }
}
