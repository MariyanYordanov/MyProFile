using System.Security.Cryptography;

public static class RefreshTokenHelper
{
    public static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public static bool IsValid(string token)
    {
        return !string.IsNullOrEmpty(token) && token.Length >= 64;
    }
}