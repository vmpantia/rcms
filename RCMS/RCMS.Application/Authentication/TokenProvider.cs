using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using RCMS.Domain.Entities;
using RCMS.Domain.Interfaces.Authentication;
using RCMS.Infrastructure.Extensions;
using RCMS.Shared;
using RCMS.Shared.Models.Users;

namespace RCMS.Application.Authentication;

public sealed class TokenProvider(JwtSetting jwtSetting, ILogger<TokenProvider> logger) : ITokenProvider
{
    public UserTokenDto Create(User user)
    {
        try
        {
            // Get token expiration
            var expires = DateTime.UtcNow.AddMinutes(jwtSetting.ExpirationInMinutes);
        
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(Constant.CLAIM_TYPE_USER_ID, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.GetFormattedName()),
                    new Claim(Constant.CLAIM_TYPE_USERNAME, user.Username),
                    new Claim(Constant.CLAIM_TYPE_INITIALS, user.GetInitials()),
                    new Claim(ClaimTypes.Email, user.EmailAddress),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                ]),
                Expires = expires,
                SigningCredentials = credentials,
                Issuer = jwtSetting.Issuer,
                Audience = jwtSetting.Audience,
            };

            var handler = new JsonWebTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);

            return new UserTokenDto(token);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while creating authentication token for user. {ex.Message}");
            throw;
        }
    }
}