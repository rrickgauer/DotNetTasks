using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Configurations;

namespace Tasks.Repositories
{
    public class DbConnection
    {
        private readonly IConfigs _configs;

        public string ConnectionString => $"server={_configs.DB_SERVER};user={_configs.DB_USER};database={_configs.DB_DATABASE};password={_configs.DB_PASSWORD}";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configs"></param>
        public DbConnection(IConfigs configs)
        {
            _configs = configs;
        }

        /// <summary>
        /// Fetch the first row from a data table (one result).
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataRow? Fetch(MySqlCommand cmd)
        {
            var results = FetchAll(cmd);

            DataRow? row = null;
            if (results.Rows.Count > 0)
            {
                row = results.Rows[0];
            }

            return row;
        }

        /// <summary>
        /// Retrieve all the data rows for the specified sql command
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataTable FetchAll(MySqlCommand cmd)
        {
            // setup a new database connection object
            using MySqlConnection conn = GetNewConnection();
            conn.Open();
            cmd.Connection = conn;

            // fill the datatable with the command results
            DataTable results = RepositoryUtils.LoadDataTable(cmd);

            conn.Close();
            conn.Dispose();

            return results;
        }

        public int Modify(MySqlCommand cmd)
        {
            // setup a new database connection object
            using MySqlConnection conn = GetNewConnection();
            conn.Open();
            cmd.Connection = conn;

            // execute the non query command
            int numRowsAffected = cmd.ExecuteNonQuery();

            // close the connection
            conn.Close();
            conn.Dispose();

            return numRowsAffected;
        }


        /// <summary>
        /// Get a new connection using the connection string
        /// </summary>
        /// <returns></returns>
        private MySqlConnection GetNewConnection()
        {
            return new MySqlConnection(ConnectionString);
        }





    }
}
