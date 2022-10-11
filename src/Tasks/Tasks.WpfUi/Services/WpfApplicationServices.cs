using System;
using System.IO;
using System.Threading.Tasks;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Services.Interfaces;
using Tasks.Utilities;

namespace Tasks.WpfUi.Services;

public class WpfApplicationServices
{
    private readonly IUserServices _userServices;
    private readonly IConfigs _configs;

    public User? User { get; private set; }

    public Guid CurrentUserId
    {
        get
        {
            var nullException = new ArgumentNullException("User object is null!!!");

            if (User is null)
                throw nullException;
            else if (!User.Id.HasValue)
                throw nullException;

            return User.Id.Value;
        }
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userServices"></param>
    /// <param name="configs"></param>
    public WpfApplicationServices(IUserServices userServices, IConfigs configs)
    {
        _userServices = userServices;
        _configs = configs;
    }

    /// <summary>
    /// Log the user in
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<bool> LogInUser(string email, string password)
    {
        User? result = await _userServices.GetUserAsync(email, password);

        if (result is null)
        {
            return false;
        }

        User = result;

        await SaveUserCredentials(email, password);

        return true;
    }

    #region Local program data direcory shit...

    /// <summary>
    /// Save the user's credentials to a file in the local program data folder
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    private async Task SaveUserCredentials(string email, string password)
    {
        SetupLocalDataDirectory();

        WpfUserCredentials credentials = new()
        {
            Email = email,
            Password = password
        };

        await JsonUtilities.WriteAsync(_configs.WpfUserCredentials.FullName, credentials, true);
    }
    
    /// <summary>
    /// Get the user's credentials from the credentials file located in the local program data directory
    /// </summary>
    /// <returns></returns>
    public async Task<WpfUserCredentials?> GetUserCredentials()
    {
        SetupLocalDataDirectory();

        var credentialsFile = _configs.WpfUserCredentials;

        if (!credentialsFile.Exists) return null;

        var parseResult = await JsonUtilities.ReadAsync<WpfUserCredentials>(credentialsFile.FullName);

        return parseResult;
    }

    /// <summary>
    /// Create the Tasks directory in the local program data folder if it does not exist
    /// </summary>
    private void SetupLocalDataDirectory()
    {
        var localAppDataFolder = _configs.LocalApplicationDataFolder;

        if (!localAppDataFolder.Exists)
        {
            Directory.CreateDirectory(localAppDataFolder.FullName);
        }
    }

    #endregion



}
