using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
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

    /// <summary>
    /// Build the api url for the given EventLabel object
    /// </summary>
    /// <param name="eventLabel"></param>
    /// <returns></returns>
    public string GetUrl(EventLabel eventLabel)
    {
        string url = $"/events/{eventLabel.EventId}/labels/{eventLabel.LabelId}";
        return url;
    }
}
