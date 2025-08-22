namespace RCMS.Shared.Models.Users;

public sealed class UserTokenDto(string token, DateTime? expires)
{
    public string Token { get; init; } = token;
    public DateTime? Expires { get; init; } = expires;
}