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

    public WpfApplicationServices(IUserServices userServices, IConfigs configs)
    {
        _userServices = userServices;
        _configs = configs;
    }

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
    

    public async Task<WpfUserCredentials?> GetUserCredentials()
    {
        SetupLocalDataDirectory();

        var credentialsFile = _configs.WpfUserCredentials;

        if (!credentialsFile.Exists) return null;

        var parseResult = await JsonUtilities.ReadAsync<WpfUserCredentials>(credentialsFile.FullName);

        return parseResult;
    }

    private void SetupLocalDataDirectory()
    {
        var localAppDataFolder = _configs.LocalApplicationDataFolder;

        if (!localAppDataFolder.Exists)
        {
            Directory.CreateDirectory(localAppDataFolder.FullName);
        }
    }

    
}
