using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.TableView;

namespace Tasks.Service.Services.Interfaces;

public interface IChecklistServices
{
    public Task<IEnumerable<ChecklistTableView>> GetUserChecklistsAsync(Guid userId);
    public Task<Checklist?> GetChecklistAsync(Guid checklistId);
}




