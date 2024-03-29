﻿using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;

namespace Tasks.Service.Services.Interfaces;

public interface IEventServices
{
    public Task<List<Event>> GetUserEventsAsync(Guid userId);
    public Task<Event?> GetEventAsync(Guid eventId, Guid userId);
    public Task<Event?> GetEventAsync(Guid eventId);
    public Task<bool> DeleteEventAsync(Guid eventId);
    public Task<Event> UpdateEventAsync(Event eventData);
    public Task<bool> ClientOwnsEventAsync(Guid eventId, Guid userId);
    public Task<Event> CreateNewEventAsync(Event eventData);
    public Task<PutEventStatus> GetPutEventStatusAsync(Guid eventId, Guid userId);
}
