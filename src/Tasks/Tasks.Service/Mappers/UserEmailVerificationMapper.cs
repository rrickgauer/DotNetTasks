using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Domain.Models;
using Tasks.Service.Mappers.Interfaces;

namespace Tasks.Service.Mappers;

public class UserEmailVerificationMapper : ModelMapper<UserEmailVerification>
{
    public override UserEmailVerification ToModel(DataRow dataRow)
    {
        UserEmailVerification userEmailVerification = new()
        {
            Id = dataRow.Field<Guid?>("id"),
            Email = dataRow.Field<string>("email"),
            UserId = dataRow.Field<Guid?>("user_id"),
            ConfirmedOn = dataRow.Field<DateTime?>("confirmed_on"),
            CreatedOn = dataRow.Field<DateTime?>("created_on"),
        };

        return userEmailVerification;
    }
}
