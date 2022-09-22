using Constants;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Methods
{
    public static class CommonMethods
    {
        public static int GetUserIdFromToken(HttpContext httpContext)
        {
            int userId = int.Parse((httpContext.User.Identity as ClaimsIdentity).FindFirst(CommonConstants.IdClaim).Value);
            return userId;
        }
    }
}