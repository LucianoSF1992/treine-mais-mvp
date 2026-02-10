using Microsoft.AspNetCore.Identity;
using TreineMais.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreineMais.Api.Data;
using TreineMais.Api.DTOs;
using TreineMais.Api.Services;
using System.Security.Cryptography;
using System.Text;

namespace TreineMais.Api.Services
{
    public class PasswordService
    {
        public string HashPassword(string senha)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(senha);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string senhaDigitada, string senhaHash)
        {
            var hashDigitado = HashPassword(senhaDigitada);
            return hashDigitado == senhaHash;
        }
    }
}
