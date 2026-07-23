using System;
using System.Security.Cryptography;

/// <summary>
/// Password hashing/verification ke liye shared helper.
/// Registration aur Login (Form.aspx.cs) dono isi class ko use karte hain,
/// taake format hamesha consistent rahe.
///
/// NOTE: Is file ko App_Code folder mein rakhein (App_Code/PasswordHelper.cs),
/// ya phir project mein kahin bhi rakh dein — bas namespace clash na ho.
/// </summary>
public static class PasswordHelper
{
    private const int SaltSize = 16;      // 128-bit salt
    private const int HashSize = 32;      // 256-bit hash
    private const int Iterations = 100000; // PBKDF2 iterations

    /// <summary>
    /// Naya password hash karta hai. Result format: "salt:hash" (dono Base64 mein).
    /// Registration aur password-change dono jagah isay call karein.
    /// </summary>
    public static string HashPassword(string password)
    {
        byte[] salt = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        byte[] hash;
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
        {
            hash = pbkdf2.GetBytes(HashSize);
        }

        return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Login ke waqt use hoga: plain password aur DB se aaya "salt:hash" (ya legacy
    /// plain-text) compare karta hai.
    /// </summary>
    public static bool VerifyPassword(string password, string storedValue)
    {
        if (string.IsNullOrEmpty(storedValue))
            return false;

        // Legacy plain-text password (migration se pehle ke users / register bug se pehle)
        if (!storedValue.Contains(":"))
        {
            return password == storedValue;
        }

        string[] parts = storedValue.Split(':');
        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] storedHash = Convert.FromBase64String(parts[1]);

        byte[] computedHash;
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
        {
            computedHash = pbkdf2.GetBytes(HashSize);
        }

        return FixedTimeEquals(computedHash, storedHash);
    }

    /// <summary>
    /// Constant-time comparison, timing attacks se bachne ke liye.
    /// </summary>
    private static bool FixedTimeEquals(byte[] a, byte[] b)
    {
        if (a.Length != b.Length)
            return false;

        int diff = 0;
        for (int i = 0; i < a.Length; i++)
        {
            diff |= a[i] ^ b[i];
        }

        return diff == 0;
    }
}
