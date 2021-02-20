using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class AccountHelper
    {
        public static string getUserId(dynamic httpContext, ClaimsPrincipal User)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            string userId = string.Empty;
            if (identity != null)
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            return userId;
        }
    }
}
