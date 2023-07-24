using Microsoft.AspNetCore.Http;

namespace Tasks.Service.Security;

public class SecurityMethods
{
    /// <summary>
    /// Get the specified request's client id from request storage
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static Guid? GetUserIdFromRequest(HttpRequest request)
    {
        Guid? clientId = null;

        if (request.HttpContext.Items.TryGetValue(RequestStorageKeys.CLIENT_USER_ID, out var potentialUserId))
        {
            clientId = (Guid)potentialUserId;
        }

        return clientId;
    }
}
