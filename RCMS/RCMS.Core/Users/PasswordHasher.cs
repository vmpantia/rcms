using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using RCMS.Core.Users.Contracts;

namespace RCMS.Core.Users;

public sealed class PasswordHasher(ILogger<PasswordHasher> logger) : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;

    private readonly HashAlgorithmName Alogrithm = HashAlgorithmName.SHA512;

    public string Hash(string password)
    {
        try
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Alogrithm, HashSize);

            var hashedPassword = $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
            return hashedPassword;
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while hashing provided password. {ex.Message}");
            throw;
        }
    }

    public bool Verify(string password, string hashedPassword)
    {
        try
        {
            var parts = hashedPassword.Split('-');
            var hash = Convert.FromHexString(parts[0]);
            var salt = Convert.FromHexString(parts[1]);

            var inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Alogrithm, HashSize);
            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
        catch (Exception ex)
        {
            logger.LogError($"Error occurred while verifying provided password and hashed password. {ex.Message}");
            return false;
        }
    }
}