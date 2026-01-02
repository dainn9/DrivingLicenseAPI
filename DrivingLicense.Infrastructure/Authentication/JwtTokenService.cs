using DrivingLicense.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DrivingLicense.Infrastructure.Authentication
{
    public class JwtTokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public JwtTokenService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string GenerateToken(string userId, string userName, IList<string> roles)
        {
            var secret = _config["JwtConfig:Secret"];
            var issuer = _config["JwtConfig:ValidIssuer"];
            var audience = _config["JwtConfig:ValidAudiences"];

            if (secret is null || issuer is null || audience is null)
            {
                throw new InvalidOperationException("Jwt is not set in the configuration");
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Name, userName),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        //public Task<bool> ValidateTokenAsync(string token)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
