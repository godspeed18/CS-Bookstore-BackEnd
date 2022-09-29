using ITPLibrary.Application.Validation.ValidationConstants;
using ITPLibrary.Domain.Entites;

namespace ITPLibrary.Application.Profiles.MappingProfilesMethods
{
    public static class MappingMethods
    {
        public static bool IsBookRecentlyAdded(DateTimeOffset dateAdded)
        {
            if ((DateTimeOffset.UtcNow - dateAdded).TotalDays <= BookValidationRules.RecentlyAddedRule)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int CalculateNumberOfItems(IEnumerable<OrderItem> items)
        {
            int total = 0;
            foreach (var item in items)
            {
                total += item.Quantity;
            }

            return total;
        }
    }
}
