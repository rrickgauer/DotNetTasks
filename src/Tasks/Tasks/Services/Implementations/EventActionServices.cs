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

        public async Task<EventAction> SaveEventCompletionAsync(Guid eventId, DateTime onDate)
        {
            EventAction eventAction = new()
            {
                EventId = eventId,
                CreatedOn = DateTime.Now,
                EventActionType = EventActionType.COMPLETION,
                OnDate = onDate,
            };

            var numRows = await _eventActionRepository.ModifyEventActionAsync(eventAction);

            return eventAction;
        }

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
    }
}
