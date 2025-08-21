using Microsoft.Extensions.Configuration;

namespace RCMS.Shared.Settings;

public class JwtSetting(string secret, string issuer, string audience, int expirationInMinutes)
{
    public string Secret { get; init; } = secret;
    public string Issuer { get; init; } = issuer;
    public string Audience { get; init; } = audience;
    public int ExpirationInMinutes { get; init; } = expirationInMinutes;

    public static JwtSetting FromConfiguration(IConfiguration configuration)
    {
        var secret = configuration[$"{nameof(JwtSetting)}:{nameof(Secret)}"]!;
        var issuer = configuration[$"{nameof(JwtSetting)}:{nameof(Issuer)}"]!;
        var audience = configuration[$"{nameof(JwtSetting)}:{nameof(Audience)}"]!;
        var expirationInMinutes = int.Parse(configuration[$"{nameof(JwtSetting)}:{nameof(ExpirationInMinutes)}"]!);

        return new JwtSetting(secret, issuer, audience, expirationInMinutes);
    }
}