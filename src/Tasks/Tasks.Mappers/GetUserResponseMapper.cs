using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Views;

namespace Tasks.Mappers
{
    public static class GetUserResponseMapper
    {
        public static GetUserResponse ToModel(DataRow dataRow)
        {
            GetUserResponse result = new()
            {
                Id = dataRow.Field<Guid?>("id"),
                Email = dataRow.Field<string?>("email"),
                Password = dataRow.Field<string?>("password"),
                CreatedOn = dataRow.Field<DateTime?>("created_on"),
                EmailConfirmedOn = dataRow.Field<DateTime?>("email_confirmed_on"),
            };

            return result;
        }
    }
}
