using System.Data;
using Tasks.Service.Domain.Constants;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Domain.Views;

using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Errors;
using Tasks.Service.Mappers;

namespace Tasks.Service.Services.Implementations;

public class UserServices : IUserServices
{
    #region Private members
    private readonly IUserRepository _userRepository;
    private static readonly GetUserResponseMapper _getUserResponseMapper = new();
    private static readonly UserMapper _userMapper = new();
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userRepository"></param>
    public UserServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    #region Update password

    /// <summary>
    /// Update the user password
    /// </summary>
    /// <param name="newPassword"></param>
    /// <returns></returns>
    public async Task<bool> UpdatePasswordAsync(Guid userId, string password)
    {
        var result = await _userRepository.UpdateUserPasswordAsync(userId, password);

        return result >= 0;
    }

    #endregion

    #region Create / Update user

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="signUpRequest"></param>
    /// <returns></returns>
    public async Task<User?> CreateUserAsync(SignUpRequest signUpRequest)
    {
        // setup a new User object with the email/password values given in the argument
        User user = new()
        {
            Id = Guid.NewGuid(),
            Email = signUpRequest.Email,
            Password = signUpRequest.Password,
            CreatedOn = DateTime.Now,
            DeliverReminders = false,
        };

        // send it to the repository
        int numRecords = await _userRepository.InsertUserAsync(user);

        return numRecords < 0 ? null : user;
    }


    #region Validate new user

    /// <summary>
    /// Validate the new user signup request
    /// </summary>
    /// <param name="signUpRequest"></param>
    /// <returns></returns>
    public async Task<ValidateUserResult> ValidateNewUserAsync(SignUpRequest signUpRequest)
    {
        var result = ValidateUserResult.Valid;

        if (!ValidateNewUserPasswordLength(signUpRequest))
        {
            return ValidateUserResult.InvalidPasswordLength;
        }

        if (signUpRequest.Email.Length > SignUpRequestValueLimits.EmailMaxLength)
        {
            return ValidateUserResult.InvalidEmailLength;
        }

        if (await IsEmailTaken(signUpRequest.Email))
        {
            return ValidateUserResult.EmailIsTaken;
        }

        return result;
    }

    /// <summary>
    /// Checks if the password value is within the limits
    /// </summary>
    /// <param name="signUpRequest"></param>
    /// <returns></returns>
    private bool ValidateNewUserPasswordLength(SignUpRequest signUpRequest)
    {
        if (signUpRequest.Password.Length < SignUpRequestValueLimits.PasswordMinLength)
        {
            return false;
        }
        else if (signUpRequest.Password.Length > SignUpRequestValueLimits.PasswordMaxLength)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if the given email address is registered to a user's account.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    private async Task<bool> IsEmailTaken(string email)
    {
        DataRow? dataRow = await _userRepository.GetUserAsync(email);

        return dataRow != null;
    }

    /// <summary>
    /// Get the appropriate SignupRequestResponse object for an invalid sign up attempt
    /// </summary>
    /// <param name="validateUserResult"></param>
    /// <returns></returns>
    public SignupRequestResponse GetInvalidSignUpRequestResponse(ValidateUserResult validateUserResult)
    {
        SignupRequestResponse result = new()
        {
            Successful = false,
            Error = GetInvalidNewUserRequestErrorMessage(validateUserResult),
        };

        return result;
    }

    /// <summary>
    /// Get the appropriate error message for the specified ValidateUserResult
    /// </summary>
    /// <param name="validateUserResult"></param>
    /// <returns></returns>
    private string GetInvalidNewUserRequestErrorMessage(ValidateUserResult validateUserResult)
    {
        string errorMessage = string.Empty;

        switch (validateUserResult)
        { 
            case ValidateUserResult.InvalidPasswordLength:
                errorMessage = InvalidSignUpRequestErrorMessages.InvalidPasswordLengthMessage;
                break;
            
            case ValidateUserResult.InvalidEmailLength:
                errorMessage = InvalidSignUpRequestErrorMessages.InvalidEmailLengthMessage;
                break;

            case ValidateUserResult.EmailIsTaken:
                errorMessage = InvalidSignUpRequestErrorMessages.EmailIsTakenMessage;
                break;
        }

        return errorMessage;
    }

    #endregion

    #endregion


    #region Get user

    /// <summary>
    /// Get a user from the repository by their user id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<User?> GetUserAsync(Guid userId)
    {
        DataRow? dataRow = await _userRepository.GetUserAsync(userId);

        if (dataRow is null) return null;

        return _userMapper.ToModel(dataRow);
    }

    /// <summary>
    /// Get the user with the email/password combination.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<User?> GetUserAsync(string email, string password)
    {
        DataRow? dataRow = await _userRepository.GetUserAsync(email, password);

        if (dataRow is null) return null;

        return _userMapper.ToModel(dataRow);
    }

    /// <summary>
    /// Get the user with the email/password combination.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<User?> GetUserAsync(string email)
    {
        DataRow? dataRow = await _userRepository.GetUserAsync(email);

        if (dataRow is null) return null;

        return _userMapper.ToModel(dataRow);
    }

    #endregion


    #region Get user view

    /// <summary>
    /// Get the specified user view
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<GetUserResponse?> GetUserViewAsync(Guid userId)
    {
        DataRow? dataRow = await _userRepository.SelectUserViewAsync(userId);

        if (dataRow is null) return null;

        GetUserResponse model = _getUserResponseMapper.ToModel(dataRow);

        return model;
    }

    #endregion

    #region Get users with reminders

    /// <summary>
    /// Get collection of users that want daily reminder emails
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<User>> GetUsersWithRemindersAsync()
    {
        DataTable dataTable = await _userRepository.SelectUsersWithRemindersAsync();

        var users = 
            from dataRow in dataTable.AsEnumerable() 
            select _userMapper.ToModel(dataRow);

        return users;
    }

    #endregion


    #region Update user

    /// <summary>
    /// Update some of a User's info with the values provided in the UpdateUserRequestForm object
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="updateUserRequestForm"></param>
    /// <returns></returns>
    /// <exception cref="TasksException"></exception>
    public async Task<bool> UpdateUserAsync(Guid userId, UpdateUserRequestForm updateUserRequestForm)
    {
        // check if the email is taken by another user
        User? userWithNewEmail = await GetUserAsync(updateUserRequestForm.Email);

        if (userWithNewEmail != null && userWithNewEmail.Id != userId)
        {
            throw new TasksException("Email is already taken");
        }

        // copy over the new fields from the UpdateUserRequestForm into the user object
        User user = new()
        {
            Id = userId,
            Email = updateUserRequestForm.Email,
            DeliverReminders = updateUserRequestForm.DeliverReminders,
            Password = string.Empty,    // repo is not updating it, however it still needs a value to run the sql command
        };

        // send it to the repository
        int numRecords = await _userRepository.UpdateUserAsync(user);

        return numRecords >= 0;
    }


    #endregion
}
