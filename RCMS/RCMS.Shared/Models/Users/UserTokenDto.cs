namespace RCMS.Shared.Models.Users;

public sealed class UserTokenDto(string token)
{
    public string Token { get; init; } = token;
}