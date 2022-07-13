using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Mappers;
using Tasks.Repositories.Interfaces;
using Tasks.Security;

#pragma warning disable CS8602 // Dereference of a possibly null reference.     

namespace Tasks.Repositories.Implementations
{
    public class EventRepository : IEventRepository
    {
        #region Sql Statements
        private class SqlStatements
        {
            public const string SELECT_ALL = @"
                SELECT
                    *
                FROM
                    Events e
                WHERE
                    e.user_id = @userId";
        }
        #endregion

        private readonly IConfigs _configs;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventRepository(IConfigs configs, IHttpContextAccessor httpContextAccessor)
        {
            _configs = configs;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Event> GetEvents()
        {
            DbConnection conn = new(_configs);
            MySqlCommand cmd = BuildCommandForGetEvents();
            DataTable results = conn.FetchAll(cmd);

            List<Event> events = new();

            foreach(DataRow dr in results.Rows)
            {
                Event e = EventMapper.ToModel(dr);
                events.Add(e);
            }

            return events;
        }

        private MySqlCommand BuildCommandForGetEvents()
        {
            MySqlCommand cmd = new(SqlStatements.SELECT_ALL);

            var userId = GetCurrentUserId();
            cmd.Parameters.Add(new("@userId", userId));

            return cmd;
        }

        private Guid? GetCurrentUserId()
        {
            return SecurityMethods.GetUserIdFromRequest(_httpContextAccessor.HttpContext.Request);
        }
    }
}
