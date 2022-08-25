﻿using Tasks.Domain.Enums;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Domain.Views;

namespace Tasks.Services.Interfaces
{
    public interface IUserServices
    {
        Task<bool> UpdatePasswordAsync(Guid userId, string password);
        Task<User?> CreateUserAsync(SignUpRequest signUpRequest);
        Task<ValidateUserResult> ValidateNewUserAsync(SignUpRequest signUpRequest);
        SignupRequestResponse GetInvalidSignUpRequestResponse(ValidateUserResult validateUserResult);
    }
}