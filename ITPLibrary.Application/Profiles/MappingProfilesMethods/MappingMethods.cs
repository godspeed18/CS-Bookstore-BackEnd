using ITPLibrary.Application.Validation.ValidationConstants;

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
    }
}
