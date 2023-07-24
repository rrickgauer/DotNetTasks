using System.Data;
using Tasks.Service.Domain.Models;
using Tasks.Service.Mappers.Interfaces;

namespace Tasks.Service.Mappers;

public class UserMapper : ModelMapper<User>
{
    /// <summary>
    /// Map the specified data row to a user domain model
    /// </summary>
    /// <param name="dataRow"></param>
    /// <returns></returns>
    public override User ToModel(DataRow dataRow)
    {
        User user = new()
        {
            Id = dataRow.Field<Guid?>("id"),
            Email = dataRow.Field<string?>("email"),
            Password = dataRow.Field<string?>("password"),
            CreatedOn = dataRow.Field<DateTime?>("created_on"),
            DeliverReminders = dataRow.Field<bool?>("deliver_reminders"),
        };

        return user;
    }

}
