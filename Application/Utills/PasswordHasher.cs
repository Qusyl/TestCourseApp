using Application.Interface;
using BCrypt.Net;
namespace Application.Utills
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        public bool Verify(string password, string HashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, HashPassword);
        }
    }
}
