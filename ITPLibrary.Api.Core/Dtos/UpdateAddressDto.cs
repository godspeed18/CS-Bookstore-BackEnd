using ITPLibrary.Api.Data.Entities.RequestMessages;
using ITPLibrary.Api.Data.Entities.ValidationRules;
using ITPLibrary.Api.Data.Entities.ValidationRules.ValidationRegex;
using System.ComponentModel.DataAnnotations;

namespace ITPLibrary.Api.Core.Dtos
{
    public class UpdateAddressDto
    {
        [RegularExpression(AddressValidationRegex.IsCountryNameValid, ErrorMessage = OrderMessages.CountryNameNotValid)]
        [MaxLength(AddressValidationRules.CountryMaxLength)]
        [MinLength(AddressValidationRules.CountryMinLength)]
        public string Country { get; set; }

        [MaxLength(AddressValidationRules.AddressLineMaxLength)]
        public string AddressLine { get; set; }


        [RegularExpression(AddressValidationRegex.IsPhoneNumberValid, ErrorMessage = OrderMessages.PhoneNumberNotValid)]
        [MaxLength(AddressValidationRules.PhoneNumberMaxLength)]
        [MinLength(AddressValidationRules.PhoneNumberMinLength)]
        public string PhoneNumber { get; set; }
    }
}
