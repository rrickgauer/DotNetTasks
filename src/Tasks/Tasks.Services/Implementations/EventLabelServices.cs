using System.Data;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Mappers;
using Tasks.Repositories.Interfaces;
using Tasks.Services.Interfaces;

namespace Tasks.Services.Implementations;

public class EventLabelServices : IEventLabelServices
{
    #region Private memebers
    private readonly IEventLabelRepository _eventLabelRepository;
    #endregion


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="eventLabelRepository"></param>
    public EventLabelServices(IEventLabelRepository eventLabelRepository)
    {
        _eventLabelRepository = eventLabelRepository;
    }

    /// <summary>
    /// Create a new EventLabel object
    /// </summary>
    /// <param name="eventLabelRequestParms"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<EventLabel?> CreateAsync(EventLabelRequestParms eventLabelRequestParms, Guid userId)
    {
        EventLabel eventLabel = (EventLabel)eventLabelRequestParms;
        eventLabel.CreatedOn = DateTime.Now;

        int numRecords = await _eventLabelRepository.InsertAsync(eventLabel, userId);

        if (numRecords <= 0)
        {
            return null;
        }

        return eventLabel;
    }


    public async Task<int> CreateBatchAsync(EventLabelsBatchRequest eventLabelsBatchRequest)
    {
        return await _eventLabelRepository.InsertBatchAsync(eventLabelsBatchRequest);
    }



    /// <summary>
    /// Get all the labels that have been assigned to the given event
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Label>> GetEventLabelsAsync(Guid eventId, Guid userId)
    {
        DataTable dataTable = await _eventLabelRepository.SelectAllAsync(eventId, userId);

        return LabelMapper.ToModels(dataTable);   
    }

    /// <summary>
    /// Build the api url for the given EventLabel object
    /// </summary>
    /// <param name="eventLabel"></param>
    /// <returns></returns>
    public string GetUri(EventLabel eventLabel)
    {
        string url = $"/events/{eventLabel.EventId}/labels/{eventLabel.LabelId}";
        return url;
    }



    /// <summary>
    /// Get all the event label assignments for the specified user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<EventLabel>> GetUserEventLabelsAsync(Guid userId)
    { 
        DataTable dataTable = await _eventLabelRepository.SelectAllAsync(userId);

        var eventLabels = EventLabelMapper.ToModels(dataTable);

        return eventLabels;        
    }
}
