using Tasks.Repositories.Interfaces;
using Tasks.Services.Interfaces;

namespace Tasks.Services.Implementations
{
    public class RecurrenceServices : IRecurrenceServices
    {
        #region Private members
        private readonly IRecurrenceRepository _recurrenceRepository;
        #endregion

        public RecurrenceServices(IRecurrenceRepository recurrenceRepository)
        {
            _recurrenceRepository = recurrenceRepository;
        }  
    }
}
