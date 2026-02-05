using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreineMais.Api.Data;
using TreineMais.Api.DTOs;

namespace TreineMais.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Senha))
            {
                return BadRequest("Email e senha são obrigatórios.");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Email == request.Email &&
                    u.Senha == request.Senha
                );

            if (user == null)
            {
                return Unauthorized("Credenciais inválidas.");
            }

            var response = new LoginResponseDto
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email,
                Tipo = user.Tipo
            };

            return Ok(response);
        }
    }
}
