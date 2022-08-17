using System.Data;
using Tasks.Domain.Enums;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Mappers;
using Tasks.Repositories.Interfaces;
using Tasks.Services.Interfaces;

namespace Tasks.Services.Implementations
{
    public class EventActionServices : IEventActionServices
    {
        #region Private members
        private readonly IEventActionRepository _eventActionRepository;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventActionRepository"></param>
        public EventActionServices(IEventActionRepository eventActionRepository)
        {
            _eventActionRepository = eventActionRepository;
        }

        /// <summary>
        /// Delete the event completion
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="onDate"></param>
        /// <returns></returns>
        public async Task<bool> DeleteEventCompletionAsync(Guid eventId, DateTime onDate)
        {
            EventAction eventAction = new()
            {
                EventId = eventId,
                CreatedOn = DateTime.Now,
                EventActionType = EventActionType.COMPLETION,
                OnDate = onDate,
            };

            var numRows = await _eventActionRepository.DeleteEventActionAsync(eventAction);

            return numRows == 1;
        }

        /// <summary>
        /// Get an event action for the specified event and date
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="onDate"></param>
        /// <returns></returns>
        public async Task<EventAction?> GetEventCompletionAsync(Guid eventId, DateTime onDate)
        {
            EventAction potentialEventAction = new()
            {
                EventId = eventId,
                CreatedOn = DateTime.Now,
                EventActionType = EventActionType.COMPLETION,
                OnDate = onDate,
            };

            DataRow? dataRow = await _eventActionRepository.GetEventActionAsync(potentialEventAction);

            EventAction? eventAction = null;

            if (dataRow != null)
            {
                eventAction = EventActionMapper.ToModel(dataRow);
            }

            return eventAction;
        }


        #region Save event action

        /// <summary>
        /// Update/Create an event completion
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="onDate"></param>
        /// <returns></returns>
        public async Task<EventAction> SaveEventCompletionAsync(Guid eventId, DateTime onDate)
        {
            var result = await SaveEventActionAsync(eventId, onDate, EventActionType.COMPLETION);

            return result;
        }

        /// <summary>
        /// Save an event cancellation to the database.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="onDate"></param>
        /// <returns></returns>
        public async Task<EventAction> SaveEventCancellationAsync(Guid eventId, DateTime onDate)
        {
            var result = await SaveEventActionAsync(eventId, onDate, EventActionType.CANCELLATION);

            return result;
        }

        /// <summary>
        /// Create a new EventAction with the specified values, and save it to the database.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="onDate"></param>
        /// <param name="eventActionType"></param>
        /// <returns></returns>
        public async Task<EventAction> SaveEventActionAsync(Guid eventId, DateTime onDate, EventActionType eventActionType)
        {
            EventAction eventAction = new()
            {
                EventId = eventId,
                CreatedOn = DateTime.Now,
                EventActionType = eventActionType,
                OnDate = onDate,
            };

            var numRows = await _eventActionRepository.ModifyEventActionAsync(eventAction);

            return eventAction;
        }

        #endregion
    }
}
