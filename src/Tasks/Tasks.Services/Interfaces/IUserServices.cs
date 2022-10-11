﻿using Tasks.Domain.Enums;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Domain.Views;

namespace Tasks.Services.Interfaces;

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
