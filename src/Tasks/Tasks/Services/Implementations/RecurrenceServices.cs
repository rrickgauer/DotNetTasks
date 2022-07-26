using System.Data;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Mappers;
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
        /// Get the recurrences 
        /// </summary>
        /// <param name="recurrenceRetrieval"></param>
        /// <returns></returns>
        public async Task<List<Recurrence>> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval)
        {
            DataTable recurrencesTable = await _recurrenceRepository.GetRecurrencesAsync(recurrenceRetrieval);

            return RecurrenceMapper.ToModels(recurrencesTable);
        }

        /// <summary>
        /// Get the event recurrences
        /// </summary>
        /// <param name="eventRecurrenceRetrieval"></param>
        /// <returns></returns>
        public async Task<List<Recurrence>> GetEventRecurrencesAsync(EventRecurrenceRetrieval eventRecurrenceRetrieval)
        {
            DataTable recurrencesTable = await _recurrenceRepository.GetEventRecurrencesAsync(eventRecurrenceRetrieval);

            return RecurrenceMapper.ToModels(recurrencesTable);
        }
    }
}
