namespace ITPLibrary.Application.Validation.ValidationConstants
{
    public static class BookValidationRules
    {
        public const int TitleMaxLength = 80;
        public const int AuthorMaxLength = 50;
        public const int PriceMin = 5;
        public const int PriceMax = 9999;
        public const int RecentlyAddedRule = 14;

        public static bool IsBookRecentlyAdded(DateTimeOffset dateAdded)
        {
            if ((DateTimeOffset.UtcNow - dateAdded).TotalDays <= RecentlyAddedRule)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
