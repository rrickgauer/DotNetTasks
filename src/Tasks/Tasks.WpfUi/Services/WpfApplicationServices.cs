using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;
using Tasks.Services.Interfaces;

namespace Tasks.WpfUi.Services;

public class WpfApplicationServices
{
    private readonly IUserServices _userServices;

    public User? User { get; private set; }

    public WpfApplicationServices(IUserServices userServices)
    {
        _userServices = userServices;
    }

    public async Task<bool> LogInUser(string email, string password)
    {
        User? result = await _userServices.GetUserAsync(email, password);

        if (result is null)
        {
            return false;
        }

        User = result;
        return true;
    }
}
