using System.Data;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;


using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Mappers;
using Tasks.Service.Domain.Responses.Custom;

namespace Tasks.Service.Services.Implementations;

public class EventLabelServices : IEventLabelServices
{
    #region Private memebers
    private readonly IEventLabelRepository _eventLabelRepository;
    private readonly ILabelServices _labelServices;

    private readonly IMapperServices _mapperServices;

    //private readonly EventLabelMapper _eventLabelMapper = new();
    //private readonly LabelMapper _labelMapper = new();
    #endregion


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="eventLabelRepository"></param>
    public EventLabelServices(IEventLabelRepository eventLabelRepository, ILabelServices eventServices, IMapperServices mapperServices)
    {
        _eventLabelRepository = eventLabelRepository;
        _labelServices = eventServices;
        _mapperServices = mapperServices;
    }

    /// <summary>
    /// Create a new EventLabel object
    /// </summary>
    /// <param name="eventLabelRequestParms"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<EventLabel?> SaveAsync(EventLabelForm eventLabelRequestParms)
    {
        EventLabel eventLabel = new()
        {
            CreatedOn = DateTime.Now,
        };

        eventLabelRequestParms.CopyFieldsToModel(eventLabel);

        int numRecords = await _eventLabelRepository.InsertAsync(eventLabel);

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
    public async Task<IEnumerable<Label>> GetAssignedLabelsAsync(Guid eventId)
    {
        DataTable dataTable = await _eventLabelRepository.SelectAllForEventAsync(eventId);

        return _mapperServices.ToModels<Label>(dataTable);   
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
    public async Task<IEnumerable<EventLabel>> GetAll(Guid userId)
    {
        DataTable dataTable = await _eventLabelRepository.SelectAllAsync(userId);

        var eventLabels = _mapperServices.ToModels<EventLabel>(dataTable);

        return eventLabels;
    }


    /// <summary>
    /// Get a list of all the user's labels, and whether or not the given event has that label assigned to it.
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<LabelAssignment>> GetLabelAssignmentsForEvent(Guid eventId, Guid userId)
    {
        var labels = await _labelServices.GetLabelsAsync(userId);
        var eventLabels = await GetAssignedLabelsAsync(eventId);
        var result = new List<LabelAssignment>();

        foreach (var label in labels)
        {
            var assignment = new LabelAssignment(label);

            var isMatch = eventLabels.Where(el => el.Id == label.Id).FirstOrDefault() != null;

            if (isMatch)
            {
                assignment.IsAssigned = true;
            }

            result.Add(assignment);
        }

        return result.OrderBy(l => l.Label.Name);
    }


    /// <summary>
    /// Delete the assignment of the event label
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="labelId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> DeleteAsync(EventLabelForm form)
    {
        var numRecords = await _eventLabelRepository.DeleteAsync(form.EventId, form.LabelId);

        if (numRecords == 1)
        {
            return true;
        }

        return false;
    }
}
