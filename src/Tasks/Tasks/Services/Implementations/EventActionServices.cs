using Tasks.Domain.Enums;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
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
        /// Save the event completion
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="eventId"></param>
        /// <param name="onDate"></param>
        /// <returns></returns>
        public EventAction SaveEventCompletion(Guid userId, Guid eventId, DateTime onDate)
        {
            EventAction eventAction = new()
            {
                EventId         = eventId,
                CreatedOn       = DateTime.Now,
                EventActionType = EventActionType.COMPLETION,
                OnDate          = onDate,
            };

            var numRows = _eventActionRepository.ModifyEventAction(eventAction);

            return eventAction;
        }


        public bool DeleteEventCompletion(Guid eventId, DateTime onDate)
        {
            EventAction eventAction = new()
            {
                EventId = eventId,
                CreatedOn = DateTime.Now,
                EventActionType = EventActionType.COMPLETION,
                OnDate = onDate,
            };

            var numRows = _eventActionRepository.DeleteEventAction(eventAction);

            return numRows == 1;
        }


    }
}
