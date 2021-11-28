using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Helpers
{
    public class AccountHelper : IAccountHelper
    {
        public string GetUserId(HttpContext httpContext, ClaimsPrincipal User)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            var userId = string.Empty;
            if (identity != null) userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }
    }
}