using System.Security.Claims;

namespace RCMS.Shared.Extensions;

public static class ClaimsExtension
{
    public static string GetClaimValue(this ClaimsPrincipal user, string claimType)
    {
        var value = user.Claims.FirstOrDefault(claim => claim.Type == claimType)?.Value;
        return value ?? string.Empty;
    }
}