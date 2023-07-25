using System.Data;

namespace Tasks.Service.Repositories.Interfaces;

public interface IChecklistRepository
{
    public Task<DataTable> SelectUserChecklistsAsync(Guid userId);
}
