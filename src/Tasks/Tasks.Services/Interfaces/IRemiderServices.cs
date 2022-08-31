using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;
using Tasks.Domain.Views;
using Tasks.Validation;

namespace Tasks.Services.Interfaces
{
    public interface IRemiderServices
    {
        public Task<IEnumerable<RecurrencesForUser>> GetRecurrencesForUsersAsync(List<User> users, IValidDateRange validDateRange);
    }
}
