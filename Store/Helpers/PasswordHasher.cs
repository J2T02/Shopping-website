using BCrypt.Net;
using Store.Exceptions;
namespace Store.Helpers
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            if (password == null) throw new ValidationException("Password is required!");
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool VerifyPassword(string password, string passwordHash)
        {
            if (password is null || passwordHash is null) return false;
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
