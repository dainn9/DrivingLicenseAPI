namespace DrivingLicense.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string userName, IList<string> roles);
        string GenerateRefreshToken();
        string HashToken(string token);
    }
}
