using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Domain.Responses.Custom;


namespace Tasks.Service.Services.Interfaces;

public interface IUserServices
{
    Task<bool> UpdatePasswordAsync(Guid userId, string password);
    Task<User?> CreateUserAsync(SignUpRequest signUpRequest);
    Task<ValidateUserResult> ValidateNewUserAsync(SignUpRequest signUpRequest);
    SignupRequestResponse GetInvalidSignUpRequestResponse(ValidateUserResult validateUserResult);
    Task<User?> GetUserAsync(Guid userId);
    Task<User?> GetUserAsync(string email, string password);
    Task<User?> GetUserAsync(string email);
    Task<GetUserResponse?> GetUserViewAsync(Guid userId);
    Task<IEnumerable<User>> GetUsersWithRemindersAsync();
    Task<bool> UpdateUserAsync(Guid userId, UpdateUserRequestForm updateUserRequestForm);
}
