using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Repositories.Interfaces;
using Tasks.Services.Interfaces;

namespace Tasks.Services.Implementations
{
    public class RecurrenceServices : IRecurrenceServices
    {
        #region Private members
        private readonly IRecurrenceRepository _recurrenceRepository;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="recurrenceRepository"></param>
        public RecurrenceServices(IRecurrenceRepository recurrenceRepository)
        {
            _recurrenceRepository = recurrenceRepository;
        }

        /// <summary>
        /// Get the recurrences for the specific event
        /// </summary>
        /// <param name="recurrenceRetrieval"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Recurrence> GetRecurrences(RecurrenceRetrieval recurrenceRetrieval)
        {
            List<Recurrence> recurrences = new();
            return recurrences;
        }

        /// <summary>
        /// Get the event recurrences for the user
        /// </summary>
        /// <param name="eventRecurrenceRetrieval"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Recurrence> GetEventRecurrences(EventRecurrenceRetrieval eventRecurrenceRetrieval)
        {
            List<Recurrence> recurrences = new();
            return recurrences;
        }


    }
}
