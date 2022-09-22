using Constants;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Common
{
    public static class CommonMethods
    {
        public static int GetUserIdFromContext(HttpContext httpContext)
        {
            int userId = int.Parse((httpContext.User.Identity as ClaimsIdentity).FindFirst(CommonConstants.IdClaim).Value);
            return userId;
        }
    }
}
