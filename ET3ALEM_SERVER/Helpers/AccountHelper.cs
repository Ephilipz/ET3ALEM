using System.Security.Claims;

namespace Helpers
{
    public class AccountHelper
    {
        public static string getUserId(dynamic httpContext, ClaimsPrincipal User)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            var userId = string.Empty;
            if (identity != null) userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }
    }
}