using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreineMais.Api.Data;
using TreineMais.Api.Models;
using TreineMais.Api.Services;

namespace TreineMais.Api.Controllers
{
    [ApiController]
    [Route("api/alunos")]
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;

        public AlunosController(
            AppDbContext context,
            PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }


        // ======================================================
        // LISTAR ALUNOS POR INSTRUTOR
        // GET: api/alunos/instrutor/{instrutorId}
        // ======================================================
        [HttpGet("instrutor/{instrutorId}")]
        public async Task<IActionResult> GetByInstrutor(int instrutorId)
        {
            var alunos = await _context.Alunos
                .Include(a => a.User)
                .Where(a => a.InstrutorId == instrutorId)
                .ToListAsync();

            return Ok(alunos);
        }

        // ======================================================
        // BUSCAR ALUNO POR ID
        // GET: api/alunos/{id}
        // ======================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var aluno = await _context.Alunos
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null)
                return NotFound();

            return Ok(aluno);
        }

        // ======================================================
        // CRIAR ALUNO
        // POST: api/alunos
        // ======================================================
        [HttpPost]
        public async Task<IActionResult> Create(Aluno aluno)
        {
            if (aluno.User == null ||
                string.IsNullOrEmpty(aluno.User.Nome) ||
                string.IsNullOrEmpty(aluno.User.Email) ||
                string.IsNullOrEmpty(aluno.User.Senha))
            {
                return BadRequest("Dados do aluno inválidos.");
            }

            aluno.User.Tipo = "ALUNO";

            // ✅ HASH DA SENHA
            aluno.User.Senha = _passwordService.HashPassword(aluno.User.Senha);

            _context.Users.Add(aluno.User);
            await _context.SaveChangesAsync();

            aluno.UserId = aluno.User.Id;

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return Ok(aluno);
        }


        // ======================================================
        // ATUALIZAR ALUNO
        // PUT: api/alunos/{id}
        // ======================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Aluno aluno)
        {
            var existente = await _context.Alunos
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (existente == null)
                return NotFound();

            if (aluno.User == null)
                return BadRequest("Dados do usuário do aluno são obrigatórios.");

            if (existente.User == null)
                return BadRequest("Usuário vinculado ao aluno não encontrado.");

            existente.User.Nome = aluno.User.Nome;
            existente.User.Email = aluno.User.Email;

            // ✅ HASH se senha informada
            if (!string.IsNullOrEmpty(aluno.User.Senha))
            {
                existente.User.Senha = _passwordService.HashPassword(aluno.User.Senha);
            }

            await _context.SaveChangesAsync();
            return Ok(existente);
        }


        // ======================================================
        // EXCLUIR ALUNO
        // DELETE: api/alunos/{id}
        // ======================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var aluno = await _context.Alunos
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (aluno == null)
                return NotFound();

            if (aluno.User != null)
                _context.Users.Remove(aluno.User);

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
