using System.Data;
using Tasks.Service.Domain.Models;
using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Domain.Enums;

namespace Tasks.Service.Services.Implementations;

public class EventServices : IEventServices
{
    #region Private members
    private readonly IEventRepository _eventRepository;
    private readonly IMapperServices _mapperServices;
    //private readonly EventMapper _eventMapper = new();
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <param name="eventRepository"></param>
    public EventServices(IEventRepository eventRepository, IMapperServices mapperServices)
    {
        _eventRepository = eventRepository;
        _mapperServices = mapperServices;
    }

    /// <summary>
    /// Delete an event
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteEventAsync(Guid eventId)
    {
        int numRowsAfftected = await _eventRepository.DeleteEventAsync(eventId);

        return numRowsAfftected > 0;
    }


    /// <summary>
    /// Get the event that is owned by the current client id
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    public async Task<Event?> GetEventAsync(Guid eventId, Guid userId)
    {
        var event_ = await GetEventAsync(eventId);

        if (event_ is null || event_.UserId != userId) return null;

        return event_;
    }

    /// <summary>
    /// Get the specified event.
    /// Returns null if the event id does not exist.
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    public async Task<Event?> GetEventAsync(Guid eventId)
    {
        DataRow? dr = await _eventRepository.GetEventAsync(eventId);

        Event? theEvent = dr != null ? _mapperServices.ToModel<Event>(dr) : null;

        return theEvent;
    }

    /// <summary>
    /// Get a list of events owned by the current user
    /// </summary>
    /// <returns></returns>
    public async Task<List<Event>> GetUserEventsAsync(Guid userId)
    {
        var dataTable = await _eventRepository.GetUserEventsAsync(userId);

        var events = _mapperServices.ToModels<Event>(dataTable);

        return events.ToList();
    }

    /// <summary>
    /// Modify the specified event
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public async Task<bool> SaveEventAsync(Event e)
    {
        int numRowsAffected = await _eventRepository.ModifyEventAsync(e);

        return numRowsAffected >= 0;
    }


    /// <summary>
    /// Create a new event
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public async Task<Event> CreateNewEventAsync(Event e)
    {
        Event newEvent = e;

        // create a new event object
        newEvent.Id = Guid.NewGuid();

        // save it in the database
        await SaveEventAsync(newEvent);

        return newEvent;
    }


    /// <summary>
    /// Check if the current user owns the event
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    public async Task<bool> ClientOwnsEventAsync(Guid eventId, Guid userId)
    {
        var e = await GetEventAsync(eventId, userId);

        if (e is null)
        {
            return false;
        }

        return true;
    }


    /// <summary>
    /// Update the event
    /// </summary>
    /// <param name="eventBody"></param>
    /// <returns></returns>
    public async Task<Event> UpdateEventAsync(Event eventBody)
    {
        // save it in the database
        await SaveEventAsync(eventBody);

        return eventBody;
    }

    /// <summary>
    /// Check if an event with this id already exists or is owned by another user.
    /// </summary>
    /// <param name="eventId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<PutEventStatus> GetPutEventStatusAsync(Guid eventId, Guid userId)
    {
        var existingEvent = await GetEventAsync(eventId);

        PutEventStatus result = PutEventStatus.Update;
        
        if (existingEvent == null)
        {
            result = PutEventStatus.Create;         // event will be created
        }
        else if (existingEvent.UserId != userId)
        {
            result = PutEventStatus.Forbid;         // event is already owned by another user
        }

        return result;
    }
}
