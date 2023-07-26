using System.Data;
using Tasks.Service.Domain.Models;

namespace Tasks.Service.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<DataRow?> GetUserAsync(string email, string password);
    public Task<DataRow?> GetUserAsync(string email);
    public Task<DataRow?> GetUserAsync(Guid userId);
    public Task<int> UpdateUserPasswordAsync(Guid userId, string password);
    public Task<int> InsertUserAsync(User user);
    public Task<DataRow?> SelectUserViewAsync(Guid userId);
    public Task<DataTable> SelectUsersWithRemindersAsync();
    public Task<int> UpdateUserAsync(User user);
}
