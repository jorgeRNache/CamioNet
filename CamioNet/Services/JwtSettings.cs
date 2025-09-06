namespace camionet.Services
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan AccessTokenExpiration { get; set; }
    }

}
