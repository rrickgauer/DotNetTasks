using System.Data;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Domain.Views;
using Tasks.Mappers;
using Tasks.Repositories.Interfaces;
using Tasks.Services.Interfaces;
using Tasks.Validation;

namespace Tasks.Services.Implementations
{
    public class RecurrenceServices : IRecurrenceServices
    {
        #region Private members
        private readonly IRecurrenceRepository _recurrenceRepository;

        private readonly IEventLabelServices _eventLabelServices;
        private readonly IEventServices _eventServices;
        private readonly ILabelServices _labelServices;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="recurrenceRepository"></param>
        public RecurrenceServices(IRecurrenceRepository recurrenceRepository, IEventLabelServices eventLabelServices, IEventServices eventServices, ILabelServices labelServices)
        {
            _recurrenceRepository = recurrenceRepository;
            _eventLabelServices = eventLabelServices;
            _eventServices = eventServices;
            _labelServices = labelServices;
        }

        /// <summary>
        /// Get the recurrences 
        /// </summary>
        /// <param name="recurrenceRetrieval"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Recurrence>> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval)
        {
            DataTable recurrencesTable = await _recurrenceRepository.GetRecurrencesAsync(recurrenceRetrieval);

            return RecurrenceMapper.ToModels(recurrencesTable);
        }

        /// <summary>
        /// Get the event recurrences
        /// </summary>
        /// <param name="eventRecurrenceRetrieval"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Recurrence>> GetEventRecurrencesAsync(EventRecurrenceRetrieval eventRecurrenceRetrieval)
        {
            DataTable recurrencesTable = await _recurrenceRepository.GetEventRecurrencesAsync(eventRecurrenceRetrieval);

            return RecurrenceMapper.ToModels(recurrencesTable);
        }


        public async Task<IEnumerable<Recurrence>> GetRecurrencesForRemindersAsync(IValidDateRange validDateRange)
        {
            DataTable recurrencesTable = await _recurrenceRepository.GetRecurrencesForRemindersAsync(validDateRange);

            return RecurrenceMapper.ToModels(recurrencesTable);
        }


        #region New stuff

        public async Task<IEnumerable<GetRecurrencesResponse>> GetRecurrencesAsync_NEW(RecurrenceRetrieval recurrenceRetrieval)
        {
            // get the user's events, labels, and EventLabels from each of the services
            var eventLabels = await _eventLabelServices.GetUserEventLabelsAsync(recurrenceRetrieval.UserId);
            var events = await _eventServices.GetUserEventsAsync(recurrenceRetrieval.UserId);
            var labels = (await _labelServices.GetLabelsAsync(recurrenceRetrieval.UserId)).Data;
            var recurrences = await GetRecurrencesAsync(recurrenceRetrieval);

            // 
            List<GetRecurrencesResponse> responses = new();

            foreach (Recurrence recurrence in recurrences)
            {
                var recurrenceResponse = BuildRecurrenceResponse(eventLabels, events, labels, recurrence);
                responses.Add(recurrenceResponse);
            }

            return responses;
        }


        private GetRecurrencesResponse BuildRecurrenceResponse(IEnumerable<EventLabel> eventLabels, IEnumerable<Event> events, IEnumerable<Label> labels, Recurrence recurrence)
        {
            // extract the event model with the matching event id from the collection of Events
            Event? e = events.FirstOrDefault(e => e.Id == recurrence.EventId);


            // get list of all the label ids from the EventLabels where the event id equal the the current event id
            // need to get a list of all the label ids that need to be included in the Event.Labels collection
            var labelIds =
                from el
                in eventLabels
                where el.EventId == recurrence.EventId
                select el.LabelId;

            // filter out the labels list to only include the ones whose ID's are within the labelIds collection
            IEnumerable<Label> labelsAssigned =
                from l
                in labels
                where labelIds.Contains(l.Id)
                select l;


            // copy over the recurrence data
            GetRecurrencesResponse result = new()
            {
                Completed = recurrence.Completed,
                OccursOn = recurrence.OccursOn,
                Cancelled = recurrence.Cancelled,
                Event = e,
                Labels = labelsAssigned,
            };

            return result;
        }


        #endregion
    }
}
