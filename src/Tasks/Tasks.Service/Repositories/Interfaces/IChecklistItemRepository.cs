﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Domain.Models;

namespace Tasks.Service.Repositories.Interfaces;

public interface IChecklistItemRepository
{
    public Task<DataTable> SelectChecklistItemsAsync(Guid checklistId);
    public Task<DataRow?> SelectChecklistItemAsync(Guid itemId);
    public Task<int> SaveChecklistItemAsync(ChecklistItem checklistItem);
    public Task<int> DeleteChecklistItemAsync(Guid itemId);
}