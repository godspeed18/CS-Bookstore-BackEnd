using Constants;
using ITPLibrary.Api.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Methods
{
    public static class CommonMethods
    {
        public static int CalculateNumberOfItems(IEnumerable<OrderItem> items)
        {
            int total = 0;
            foreach (var item in items)
            {
                total += item.Quantity;
            }

            return total;
        }

        public static int GetUserIdFromToken(HttpContext httpContext)
        {
            int userId = int.Parse((httpContext.User.Identity as ClaimsIdentity).FindFirst(CommonConstants.IdClaim).Value);
            return userId;
        }
    }
}