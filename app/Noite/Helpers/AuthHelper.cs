using System.Security.Claims;

namespace Noite.Helpers;

public abstract class AuthHelper
{
    public static string UserId(ClaimsPrincipal user)
    {
        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception("Invalid user token");

        return id;
    }
}
