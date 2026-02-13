namespace DrivingLicense.Infrastructure.Authentication
{
    public class JwtConfig
    {
        public string Secret { get; set; } = string.Empty;
        public string ValidIssuer { get; set; } = string.Empty;
        public string ValidAudience { get; set; } = string.Empty;
        public int AccessTokenExpirationMinutes { get; set; }
        public int RefreshTokenExpirationDays { get; set; }
    }
}
