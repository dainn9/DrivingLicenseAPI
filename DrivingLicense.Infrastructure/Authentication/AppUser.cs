using Microsoft.AspNetCore.Identity;

namespace DrivingLicense.Infrastructure.Authentication
{
    public class AppUser : IdentityUser
    {
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}