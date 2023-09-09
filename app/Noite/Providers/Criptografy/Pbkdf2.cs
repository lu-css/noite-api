using System.Security.Cryptography;

namespace Noite.Providers.Criptografy;

public class Pbkdf2 : ICriptografyProvider
{
    private readonly int SALT_LENGTH = 16;
    private readonly int INTERATIONS = 10000;

    public byte[] GenenateSalt()
    {
        byte[] salt = new byte[SALT_LENGTH];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
            return salt;
        }
    }

    public (string HashedPassword, string Password) Hash(string password, byte[] salt)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, INTERATIONS, HashAlgorithmName.SHA256))
        {
            byte[] hashedPassword = pbkdf2.GetBytes(32); // 256 bits
            return (Convert.ToBase64String(hashedPassword), Convert.ToBase64String(salt));
        }
    }

    public (string HashedPassword, string Password) HashPassword(string password)
    {
        var salt = GenenateSalt();
        return Hash(password, salt);
    }

    public bool VerifyPassword(string password, string storedHashedPassword, string storedSalt)
    {
        byte[] saltBytes = Convert.FromBase64String(storedSalt);
        var hashedPassword = Hash(password, saltBytes).HashedPassword;
        return hashedPassword == storedHashedPassword;
    }
}
