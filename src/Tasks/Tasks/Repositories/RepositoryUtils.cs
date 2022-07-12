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

        public static DataTable LoadDataTable(MySqlCommand cmd)
        {
            DataTable dt = new();
            
            dt.Load(cmd.ExecuteReader());
            
            return dt;
        }
    }
}
