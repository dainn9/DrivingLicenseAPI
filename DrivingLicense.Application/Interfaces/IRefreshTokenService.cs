using DrivingLicense.Infrastructure.Authentication;

namespace DrivingLicense.Application.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<string> CreateRefreshTokenAsync(string userId);
        Task<string> ValidateAndRevokeAsync(string refreshToken);
        Task<(string userId, string newRefreshToken)> RotateRefreshTokenAsync(string refreshToken);
    }
}
