using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Configurations;

namespace Tasks.Repositories
{
    public class RepositoryUtils
    {
        public static MySqlConnection GetConnection(IConfigs configs)
        {
            string connectionString = GetConnectionString(configs);
            MySqlConnection conn = new(connectionString);
            return conn;
        }

        public static string GetConnectionString(IConfigs configs)
        {
            return $"server={configs.DB_SERVER};user={configs.DB_USER};database={configs.DB_DATABASE};password={configs.DB_PASSWORD}";
        }

        public static DataTable LoadDataTable(MySqlCommand command)
        {
            DataTable dataTable = new();
            
            dataTable.Load(command.ExecuteReader());
            
            return dataTable;
        }


        public static async Task<DataTable> LoadDataTableAsync(MySqlCommand command)
        {
            DataTable dataTable = new();

            var reader = await command.ExecuteReaderAsync();
            dataTable.Load(reader);

            return dataTable;
        }
    }
}
