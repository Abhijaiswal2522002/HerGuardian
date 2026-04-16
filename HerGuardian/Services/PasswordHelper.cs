using System.Security.Cryptography;
using System.Text;

namespace HerGuardian.Services
{
    public class PasswordHelper
    {
        public static string HashPassword(string Password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(Password);
            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        public static bool VerifyPassword(string inputPassword,string storedHash)
        {
            var hash = HashPassword(inputPassword);
            return hash == storedHash;
        }
    }
}
