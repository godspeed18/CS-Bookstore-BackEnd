namespace ITPLibrary.Api.Core.Dtos
{
    public class CreditCardDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardNumber { get; set; }
        public int ExpirationYear { get; set; }
        public int ExpirationMonth { get; set; }
        public int CVV2 { get; set; }
    }
}
