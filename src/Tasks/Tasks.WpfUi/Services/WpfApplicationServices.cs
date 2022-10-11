using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Tasks.Configurations;
using Tasks.Domain.Models;
using Tasks.Services.Interfaces;
using Tasks.Utilities;

namespace Tasks.WpfUi.Services;

public class WpfApplicationServices
{
    private readonly IUserServices _userServices;
    private readonly IConfigs _configs;

    public List<string> CliArgs { get; set; } = new();

    public User? User { get; private set; }

    /// <summary>
    /// Get the current User's ID
    /// </summary>
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

    /// <summary>
    /// Log the user out
    /// </summary>
    public void Logout()
    {
        // delete the credentials file
        DeleteUserCredentialsFile();

        // start up a new process
        StartNewProcess();

        // shut down the current process
        Application.Current.Shutdown();
    }

    /// <summary>
    /// Start up a new process
    /// </summary>
    private void StartNewProcess()
    {
        ProcessStartInfo startInfo = new()
        {
            FileName = _configs.WpfApplicationExe.FullName,
        };

        foreach (var arg in CliArgs)
        {
            startInfo.ArgumentList.Add(arg);
        }

        Process.Start(startInfo);
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
    /// Get the user's credentials from the credentials file located in the local program data directory.
    /// Returns null if the credentials file was not found or is empty, etc...
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

    /// <summary>
    /// Delete the user credentials file if it exists.
    /// </summary>
    public void DeleteUserCredentialsFile()
    {
        var credentialsFile = _configs.WpfUserCredentials;

        if (!credentialsFile.Exists) return;

        File.Delete(credentialsFile.FullName);
    }

    #endregion



}
