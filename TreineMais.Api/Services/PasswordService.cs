using Microsoft.AspNetCore.Identity;
using TreineMais.Api.Models;

namespace TreineMais.Api.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<User> _hasher = new();

        public string HashPassword(string senha)
        {
            return _hasher.HashPassword(null!, senha);
        }

        public bool VerifyPassword(User user, string senhaHash, string senhaDigitada)
        {
            var result = _hasher.VerifyHashedPassword(user, senhaHash, senhaDigitada);
            return result == PasswordVerificationResult.Success;
        }
    }
}
