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

            var admin = new User
            {
                Nome = "Administrador",
                Email = "admin@treinemais.com",
                Senha = senhaHash,
                Tipo = "Admin"
            };

            admin.Senha = passwordService.HashPassword(request.Senha);

            context.Users.Add(admin);
            context.SaveChanges();
        }
    }
}
