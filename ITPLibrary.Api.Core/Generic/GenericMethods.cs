using ITPLibrary.Api.Data.Entities;

namespace ITPLibrary.Api.Core.Generic
{
    public static class GenericMethods
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
    }
}
