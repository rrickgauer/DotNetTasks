using System.Data;
using System.Linq;
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

        #region Get recurrences for email

        /// <summary>
        /// Get the recurrences for reminders
        /// </summary>
        /// <param name="validDateRange"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Recurrence>> GetRecurrencesForRemindersAsync(IValidDateRange validDateRange)
        {
            DataTable recurrencesTable = await _recurrenceRepository.GetRecurrencesForRemindersAsync(validDateRange);

            return RecurrenceMapper.ToModels(recurrencesTable);
        }

        #endregion



        #region Get Recurrences

        /// <summary>
        /// Get all recurrence for the specified time period
        /// </summary>
        /// <param name="recurrenceRetrieval"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetRecurrencesResponse>> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval)
        {
            // get the user's events, labels, and EventLabels from each of the services
            IEnumerable<EventLabel> eventLabels = await _eventLabelServices.GetUserEventLabelsAsync(recurrenceRetrieval.UserId);
            List<Event> events = await _eventServices.GetUserEventsAsync(recurrenceRetrieval.UserId);
            IEnumerable<Label> labels = (await _labelServices.GetLabelsAsync(recurrenceRetrieval.UserId)).Data;
            IEnumerable<Recurrence>? recurrences = await GetRecurrecesFromDbAsync(recurrenceRetrieval);
            
            List<GetRecurrencesResponse> responses = new();

            foreach (Recurrence recurrence in recurrences)
            {
                Event? e = events.FirstOrDefault(e => e.Id == recurrence.EventId);

                if (e is null) continue;

                var response = BuildRecurrenceResponse(eventLabels, labels, e, recurrence);

                if (recurrenceRetrieval.LabelIds == null)
                {
                    responses.Add(response);
                    continue;
                }

                // only add events that are assigned one of the labels requested
                foreach (var recurrenceRetrievalLabelId in recurrenceRetrieval.LabelIds)
                {
                    if (response.Labels.Select(l => l.Id).Contains(recurrenceRetrievalLabelId))
                    {
                        responses.Add(response);
                        continue;
                    }
                }
            }

            return responses;
        }

        /// <summary>
        /// Get the recurrences 
        /// </summary>
        /// <param name="recurrenceRetrieval"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Recurrence>> GetRecurrecesFromDbAsync(RecurrenceRetrieval recurrenceRetrieval)
        {
            DataTable recurrencesTable = await _recurrenceRepository.GetRecurrencesAsync(recurrenceRetrieval);

            return RecurrenceMapper.ToModels(recurrencesTable);
        }

        #endregion


        #region Get Event Recurrences


        /// <summary>
        /// Get all the recurrences for an event
        /// </summary>
        /// <param name="recurrenceRetrieval"></param>
        /// <returns></returns>
        public async Task<IEnumerable<GetRecurrencesResponse>> GetRecurrencesAsync(RecurrenceRetrieval recurrenceRetrieval, Guid eventId)
        {
            // get the user's events, labels, and EventLabels from each of the services
            var eventLabels = await _eventLabelServices.GetUserEventLabelsAsync(recurrenceRetrieval.UserId);
            var labels = (await _labelServices.GetLabelsAsync(recurrenceRetrieval.UserId)).Data;
            var recurrences = await GetRecurrecesFromDbAsync(recurrenceRetrieval, eventId);

            Event? e = await _eventServices.GetEventAsync(eventId);

            List<GetRecurrencesResponse> responses = new();

            foreach (Recurrence recurrence in recurrences)
            {
                var recurrenceResponse = BuildRecurrenceResponse(eventLabels, labels, e, recurrence);

                responses.Add(recurrenceResponse);
            }

            return responses;
        }


        /// <summary>
        /// Get the event recurrences
        /// </summary>
        /// <param name="eventRecurrenceRetrieval"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Recurrence>> GetRecurrecesFromDbAsync(RecurrenceRetrieval eventRecurrenceRetrieval, Guid eventId)
        {
            DataTable recurrencesTable = await _recurrenceRepository.GetRecurrencesAsync(eventRecurrenceRetrieval, eventId);

            return RecurrenceMapper.ToModels(recurrencesTable);
        }


        #endregion


        #region Build Recurrence Response

        /// <summary>
        /// Build a new GetRecurrencesResponse
        /// </summary>
        /// <param name="eventLabels"></param>
        /// <param name="labels"></param>
        /// <param name="e"></param>
        /// <param name="recurrence"></param>
        /// <returns></returns>
        private GetRecurrencesResponse BuildRecurrenceResponse(IEnumerable<EventLabel> eventLabels, IEnumerable<Label> labels, Event e, Recurrence recurrence)
        {
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
