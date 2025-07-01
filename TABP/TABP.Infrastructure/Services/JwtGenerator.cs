using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TABP.Domain.Entites;
using TABP.Domain.Interfaces.Services;
using TABP.Infrastructure.Common;
namespace TABP.Infrastructure.Services
{
    public class JwtGenerator(IOptionsSnapshot<JwtConfigurations> jwtSettings) : IJwtGenerator
    {
        public string GenerateToken(User user)
        {
            var authClaims = GetClaims(user);
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.Key));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var expirationTime = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings.Value.ExpiresInMinutes));
            var token = new JwtSecurityToken(
                issuer: jwtSettings.Value.Issuer,
                audience: jwtSettings.Value.Audience,
                claims: authClaims.Claims,
                expires: expirationTime,
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public ClaimsIdentity GetClaims(User user)
        {
            var authClaims = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            ]);
            return authClaims;
        }
    }
}