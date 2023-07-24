using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Views;
using Tasks.Service.Validation;

namespace Tasks.Service.Services.Interfaces;

public interface IRemiderServices
{
    public Task<IEnumerable<UserRecurrences>> GetRecurrencesForUsersAsync(IEnumerable<User> users, IValidDateRange validDateRange);
}
