using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Domain.Models;
using Tasks.Service.Domain.Parms;
using Tasks.Service.Domain.Views;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Validation;

namespace Tasks.Service.Services.Implementations;


#pragma warning disable CS8629 // Nullable value type may be null.

public class ReminderServices : IRemiderServices
{
    private readonly IRecurrenceServices _recurrenceServices;

    public ReminderServices(IRecurrenceServices recurrenceServices)
    {
        _recurrenceServices = recurrenceServices;
    }

    public async Task<IEnumerable<UserRecurrences>> GetRecurrencesForUsersAsync(IEnumerable<User> users, IValidDateRange validDateRange)
    {
        // put all the users into a dictionary to access their user ids
        Dictionary<Guid, UserRecurrences> resultDict = new();

        foreach (var user in users)
        {
            resultDict.TryAdd(user.Id.Value, new(user));
        }

        // add all the resources to to the users dict
        IEnumerable<Recurrence> recurrences = await _recurrenceServices.GetRecurrencesForRemindersAsync(validDateRange);

        foreach (var recurrence in recurrences)
        {
            if (resultDict.TryGetValue(recurrence.UserId.Value, out UserRecurrences? userRecurrences))
            {
                userRecurrences.Recurrences.Add(recurrence);
            }
        }

        // filter out the results to users that have recurrences
        var filteredUsers = 
            from userRecurrences in resultDict.Values
            where userRecurrences.Recurrences is not null
            && userRecurrences.Recurrences.Count > 0
            select userRecurrences;

        return filteredUsers;
    }


}
