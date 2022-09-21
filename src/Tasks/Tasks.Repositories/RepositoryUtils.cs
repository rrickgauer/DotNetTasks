using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using Tasks.Configurations;

namespace Tasks.Repositories
{
    public class RepositoryUtils
    {
        public static MySqlConnection GetConnection(IConfiguration configs)
        {
            string connectionString = GetConnectionString(configs);
            MySqlConnection conn = new(connectionString);
            return conn;
        }

        public static string GetConnectionString(IConfiguration configs)
        {
            return $"server={configs.DbServer};user={configs.DbUser};database={configs.DbDataBase};password={configs.DbPassword}";
        }

        public static async Task<DataTable> LoadDataTableAsync(MySqlCommand command)
        {
            DataTable dataTable = new();

            DbDataReader reader = await command.ExecuteReaderAsync();
            dataTable.Load(reader);

            return dataTable;
        }
    }
}
