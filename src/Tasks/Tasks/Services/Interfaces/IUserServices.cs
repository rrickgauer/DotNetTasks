namespace Tasks.Services.Interfaces
{
    public interface IUserServices
    {
        Task<bool> UpdatePasswordAsync(Guid userId, string password);
    }
}
