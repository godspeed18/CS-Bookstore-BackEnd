using System.Security.Claims;
using ITPLibrary.Api.Core.GenericConstants;

namespace ITPLibrary.Api.Generic
{
    public static class GenericMethods
    {
        public static int GetUserIdFromToken(HttpContext httpContext)
        {
            int userId = int.Parse((httpContext.User.Identity as ClaimsIdentity).FindFirst(GenericConstant.IdClaim).Value);
            return userId;
        }
    }
}
