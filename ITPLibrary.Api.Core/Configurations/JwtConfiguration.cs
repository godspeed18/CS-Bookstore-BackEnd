namespace ITPLibrary.Api.Data.Configurations
{
    public class JwtConfiguration
    {
        public string Key { get; set; }
        public string Subject { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
