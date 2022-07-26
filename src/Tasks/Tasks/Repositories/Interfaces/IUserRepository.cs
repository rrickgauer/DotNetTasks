using Tasks.Domain.Models;

namespace Tasks.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User?> GetUserAsync(string email, string password);
    }
}
