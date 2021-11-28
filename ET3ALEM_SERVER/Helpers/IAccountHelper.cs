using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Helpers
{
    public interface IAccountHelper
    {
        string GetUserId(HttpContext httpContext, ClaimsPrincipal User);
    }
}