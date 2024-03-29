﻿using System.Data;
using Tasks.Service.Domain.Models;

namespace Tasks.Service.Repositories.Interfaces;

public interface IChecklistRepository
{
    public Task<DataTable> SelectUserChecklistsAsync(Guid userId);
    public Task<DataRow?> SelectChecklistAsync(Guid checklistId);
    public Task<int> SaveChecklistAsync(Checklist checklist);
    public Task<int> DeleteChecklistAsync(Guid checklistId);
    public Task<DataRow?> SelectChecklistByCommandLineReferenceAsync(uint commandLineReference);
}
