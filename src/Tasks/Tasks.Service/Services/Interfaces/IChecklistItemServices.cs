﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Domain.Models;

namespace Tasks.Service.Services.Interfaces;

public interface IChecklistItemServices
{
    public Task<IEnumerable<ChecklistItem>> GetChecklistItemsAsync(Guid checklistId);
    public Task<ChecklistItem?> GetChecklistItemAsync(Guid itemId);
}
