using MySql.Data.MySqlClient;
using System.Data;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Mappers;
using Tasks.Repositories.Interfaces;

namespace Tasks.Repositories.Implementations
{
    public class EventRepository : IEventRepository
    {
        private IConfigs _configs;

        public EventRepository(IConfigs configs)
        {
            _configs = configs;
        }

        public List<Event> GetEvents()
        {
            DbConnection conn = new(_configs);
            MySqlCommand cmd = new("Select * From Events");
            DataTable results = conn.FetchAll(cmd);

            var events = new List<Event>();

            foreach(DataRow dr in results.Rows)
            {
                Event e = EventMapper.ToModel(dr);
                events.Add(e);
            }

            return events;
        }
    }
}
