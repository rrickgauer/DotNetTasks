using Tasks.Domain.Models;

namespace Tasks.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> GetUserAsync(string email, string password);
        public Task<User?> GetUserAsync(string email);
        public Task<User?> GetUserAsync(Guid userId);
        public Task<int> UpdateUserPasswordAsync(Guid userId, string password);
        public Task<int> InsertUserAsync(User user);
    }
}
