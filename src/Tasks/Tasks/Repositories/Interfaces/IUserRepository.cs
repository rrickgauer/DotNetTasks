using Tasks.Domain.Models;

namespace Tasks.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public User? GetUser(string email, string password);
    }
}
