using System.Data;
using Tasks.Service.Domain.Views;
using Tasks.Service.Mappers.Interfaces;

namespace Tasks.Service.Mappers;

public class GetUserResponseMapper : ModelMapper<GetUserResponse>
{
    public override GetUserResponse ToModel(DataRow dataRow)
    {
        GetUserResponse result = new()
        {
            Id = dataRow.Field<Guid?>("id"),
            Email = dataRow.Field<string?>("email"),
            Password = dataRow.Field<string?>("password"),
            CreatedOn = dataRow.Field<DateTime?>("created_on"),
            EmailConfirmedOn = dataRow.Field<DateTime?>("email_confirmed_on"),
            DeliverReminders = dataRow.Field<bool?>("deliver_reminders"),
        };

        return result;
    }
}
