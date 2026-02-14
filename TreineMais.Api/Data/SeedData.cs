using TreineMais.Api.Models;
using TreineMais.Api.Services;

namespace TreineMais.Api.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context, PasswordService passwordService)
        {
            if (context.Users.Any())
                return;

            var senhaHash = passwordService.HashPassword("123456");

            var admin = new User
            {
                Nome = "Administrador",
                Email = "admin@treinemais.com",
                Senha = senhaHash,
                Tipo = "Admin"
            };

            context.Users.Add(admin);
            context.SaveChanges();
        }
    }
}
