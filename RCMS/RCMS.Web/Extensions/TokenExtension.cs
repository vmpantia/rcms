using System.Security.Claims;
using RCMS.Shared;

namespace RCMS.Web.Extensions;

public static class TokenExtension
{
    public static string GetClaimValue(this ClaimsPrincipal user, string claimType)
    {
        var value = user.Claims.FirstOrDefault(claim => claim.Type == claimType)?.Value;
        return value ?? string.Empty;
    }
    
    public static Task<bool> IsValidAsync(this ClaimsPrincipal user)
    {
        if (user == null || !user.Identity.IsAuthenticated)
            return Task.FromResult(false);

        var expClaim = user.FindFirst(Constant.CLAIM_TYPE_EXPIRATION)?.Value;
        if (string.IsNullOrEmpty(expClaim))
            return Task.FromResult(false);

        if (long.TryParse(expClaim, out var expUnix))
        {
            var expiration = DateTimeOffset.FromUnixTimeSeconds(expUnix);
            return Task.FromResult(expiration > DateTimeOffset.UtcNow);
        }

        return Task.FromResult(false);
    }

}