namespace ITPLibrary.Api.Data.Entities.ValidationRules.ValidationRegex
{
    public static class AddressValidationRegex
    {
        public const string IsPhoneNumberValid = "^\\+?[1-9][0-9]{7,14}$";
        public const string IsCountryNameValid = "^[a-zA-Z]+$";
    }
}
