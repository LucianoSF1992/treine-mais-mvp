using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreineMais.Api.Data;
using TreineMais.Api.DTOs;
using Microsoft.AspNetCore.Identity;
using TreineMais.Api.Services;


namespace TreineMais.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly PasswordService _passwordService;
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context, PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Senha))
            {
                return BadRequest("Email e senha são obrigatórios.");
            }

            var user = await _context.Users
    .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || string.IsNullOrEmpty(user.Senha))
                return Unauthorized("Credenciais inválidas.");

            var senhaValida = _passwordService.VerifyPassword(request.Senha, user.Senha);

            if (!senhaValida)
                return Unauthorized("Credenciais inválidas.");

            var response = new LoginResponseDto
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email,
                Tipo = user.Tipo
            };

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Nome) ||
                string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.Senha) ||
                string.IsNullOrEmpty(request.Tipo))
            {
                return BadRequest("Todos os campos são obrigatórios.");
            }

            var userExists = await _context.Users
                .AnyAsync(u => u.Email == request.Email);

            if (userExists)
            {
                return BadRequest("Email já cadastrado.");
            }

            var user = new Models.User
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = _passwordService.HashPassword(request.Senha),
                Tipo = request.Tipo
            };


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Usuário cadastrado com sucesso.");
        }
    }
}
