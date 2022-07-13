//using Microsoft.AspNetCore.Mvc;
using System.Web;

using Microsoft.AspNetCore.Mvc;
namespace Tasks.Security
{
    public class SecurityMethods
    {
        public static Guid? GetCurrentClientId(HttpRequest request)
        {
            Guid? clientId = null;

            if (request.HttpContext.Items.TryGetValue(RequestStorageKeys.CLIENT_USER_ID, out var userIdtry))
            {
                clientId = (Guid)userIdtry;
            }

            return clientId;
        }

    }
}
