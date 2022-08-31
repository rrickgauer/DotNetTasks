using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;
using Tasks.Domain.Parms;
using Tasks.Domain.Views;
using Tasks.Services.Interfaces;
using Tasks.Validation;

namespace Tasks.Services.Implementations;


#pragma warning disable CS8629 // Nullable value type may be null.

public class ReminderServices : IRemiderServices
{
    private readonly IRecurrenceServices _recurrenceServices;

    public ReminderServices(IRecurrenceServices recurrenceServices)
    {
        _recurrenceServices = recurrenceServices;
    }

    public async Task<IEnumerable<RecurrencesForUser>> GetRecurrencesForUsersAsync(List<User> users, IValidDateRange validDateRange)
    {
        RecurrenceRetrieval recurrenceRetrieval = new()
        {
            StartsOn = validDateRange.StartsOn,
            EndsOn = validDateRange.EndsOn,
        };


        List<RecurrencesForUser> recurrencesForUsers = new();

        foreach (var user in users)
        {
            recurrenceRetrieval.UserId = user.Id.Value;

            List<Recurrence> recurrences = await _recurrenceServices.GetRecurrencesAsync(recurrenceRetrieval);

            recurrencesForUsers.Add(new(user, recurrences));
        }

        return recurrencesForUsers;
    }


}
