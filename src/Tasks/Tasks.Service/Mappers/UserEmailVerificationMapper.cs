using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Domain.Models;

namespace Tasks.Service.Mappers;

public static class UserEmailVerificationMapper
{
    public static UserEmailVerification ToModel(DataRow dataRow)
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
