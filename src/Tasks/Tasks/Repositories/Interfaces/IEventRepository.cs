using System.Data;
using Tasks.Domain.Models;
namespace Tasks.Repositories.Interfaces
{
    public interface IEventRepository
    {
        //public DataTable GetUserEvents(Guid userId);
        //public int DeleteEvent(Guid eventId);
        //public int ModifyEvent(Event e);
        //public DataRow? GetEvent(Guid eventId);





        public DataTable GetUserEvents(Guid userId);
        public Task<DataTable> GetUserEventsAsync(Guid userId);


        public int DeleteEvent(Guid eventId);
        public int ModifyEvent(Event e);
        public DataRow? GetEvent(Guid eventId);

    }
}
