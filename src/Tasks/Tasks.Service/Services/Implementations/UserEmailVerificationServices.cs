using System.Data;
using Tasks.Service.Configurations;
using Tasks.Service.Domain.Models;
using Tasks.Service.Email;
using Tasks.Service.Email.Messages;

using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Mappers;

#pragma warning disable CS8601 // Possible null reference assignment.

namespace Tasks.Service.Services.Implementations;

public class UserEmailVerificationServices : IUserEmailVerificationServices
{
    #region Private members
    private readonly IUserServices _userServices;
    private readonly IUserEmailVerificationRepository _userEmailVerificationRepository;
    private readonly IConfigs _configs;
    #endregion

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userServices"></param>
    public UserEmailVerificationServices(IUserServices userServices, IUserEmailVerificationRepository userEmailVerificationRepository, IConfigs configs)
    {
        _userServices = userServices;
        _userEmailVerificationRepository = userEmailVerificationRepository;
        _configs = configs;
    }

    /// <summary>
    /// Create a new email verification record in the database for the speicfied user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<UserEmailVerification?> CreateNewAsync(Guid userId)
    {
        // need to the get the user's email address
        User? user = await _userServices.GetUserAsync(userId);

        if (user is null) return null;

        UserEmailVerification newUserEmailVerification = BuildNewVerificationObjectFromUser(user);

        // save the record to the database
        int numRecords = await _userEmailVerificationRepository.InsertAsync(newUserEmailVerification);

        if (numRecords < 0)
        {
            throw new Exception("InsertAsync returned less than 0 records...");
        }

        return newUserEmailVerification;
    }

    /// <summary>
    /// Build a brand new UserEmailVerification object with values from the specifed user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private static UserEmailVerification BuildNewVerificationObjectFromUser(User user)
    {
        UserEmailVerification emailVerification = new()
        {
            Id = Guid.NewGuid(),
            Email = user.Email,
            UserId = user.Id,
            CreatedOn = DateTime.Now,
            ConfirmedOn = null,
        };

        return emailVerification;
    }

    /// <summary>
    /// Send the email confirmation message
    /// </summary>
    /// <param name="userEmailVerification"></param>
    /// <returns></returns>
    public async Task<bool> SendEmailAsync(UserEmailVerification userEmailVerification)
    {
        EmailServer server = new(_configs);
        server.Connect();

        UserEmailVerificationMessage message = new(userEmailVerification, _configs);
        await server.SendMessageAsync(message);

        server.CloseConnection();

        return true;
    }

    /// <summary>
    /// Confirm the given UserEmailVerification record 
    /// </summary>
    /// <param name="userEmailVerificationId"></param>
    /// <returns></returns>
    public async Task<UserEmailVerification?> ConfirmEmailAsync(Guid userEmailVerificationId)
    {
        // make sure the id exists and is valid
        UserEmailVerification? userEmailVerification = await GetUserEmailVerificationAsync(userEmailVerificationId);

        if (userEmailVerification is null) 
        {
            return null;
        }

        // don't do anything if it's already confirmed
        if (userEmailVerification.IsConfirmed)
        {
            return userEmailVerification;
        }

        userEmailVerification.ConfirmedOn = DateTime.Now;

        await _userEmailVerificationRepository.UpdateAsync(userEmailVerification);

        return userEmailVerification;
    }

    /// <summary>
    /// Get the specified UserEmailVerification record
    /// </summary>
    /// <param name="userEmailVerificationId"></param>
    /// <returns></returns>
    public async Task<UserEmailVerification?> GetUserEmailVerificationAsync(Guid userEmailVerificationId)
    {
        DataRow? dataRow = await _userEmailVerificationRepository.GetAsync(userEmailVerificationId);

        if (dataRow is null) return null;

        var model = UserEmailVerificationMapper.ToModel(dataRow);

        return model;
    }
}
