using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreineMais.Api.Data;
using TreineMais.Api.Models;

namespace TreineMais.Api.Controllers
{
    [ApiController]
    [Route("api/alunos")]
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
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

            existente.User.Nome = aluno.User.Nome;
            existente.User.Email = aluno.User.Email;

            if (!string.IsNullOrEmpty(aluno.User.Senha))
                existente.User.Senha = aluno.User.Senha;

            existente.Idade = aluno.Idade;
            existente.Objetivo = aluno.Objetivo;

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

            _context.Users.Remove(aluno.User);
            _context.Alunos.Remove(aluno);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
