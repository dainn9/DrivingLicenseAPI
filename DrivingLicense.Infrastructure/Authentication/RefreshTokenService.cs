using DrivingLicense.Application.Common.Exceptions;
using DrivingLicense.Application.Interfaces;
using DrivingLicense.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DrivingLicense.Infrastructure.Authentication
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly DrivingDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly JwtConfig _jwtConfig;

        public RefreshTokenService(DrivingDbContext context, ITokenService tokenService, IOptions<JwtConfig> jwtOptions)
        {
            _context = context;
            _tokenService = tokenService;
            _jwtConfig = jwtOptions.Value;
        }

        public async Task<string> CreateRefreshTokenAsync(string userId)
        {
            var refreshToken = _tokenService.GenerateRefreshToken();
            var hashedRefreshToken = _tokenService.HashToken(refreshToken);

            var refreshTokenEnity = new RefreshToken
            {
                Token = hashedRefreshToken,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtConfig.RefreshTokenExpirationDays),
                IsRevoked = false
            };

            _context.RefreshTokens.Add(refreshTokenEnity);

            await _context.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<(string userId, string newRefreshToken)> RotateRefreshTokenAsync(string refreshToken)
        {
            var userId = await ValidateAndRevokeAsync(refreshToken);

            var newRefreshToken = await CreateRefreshTokenAsync(userId);

            return (userId, newRefreshToken);
        }

        public async Task<string> ValidateAndRevokeAsync(string refreshToken)
        {
            var hashedToken = _tokenService.HashToken(refreshToken);
            var storedRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rf => rf.Token == hashedToken);

            if (storedRefreshToken == null
                 || storedRefreshToken.IsRevoked
                 || storedRefreshToken.ExpiresAt < DateTime.UtcNow)
            {
                throw new UnauthorizedException("Invalid refresh token.");
            }

            storedRefreshToken.IsRevoked = true;
            storedRefreshToken.RevokedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return storedRefreshToken.UserId;
        }
    }
}