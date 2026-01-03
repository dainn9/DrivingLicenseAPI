namespace DrivingLicense.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string userName, IList<string> roles);

        //Task<bool> ValidateTokenAsync(string token);
    }
}
