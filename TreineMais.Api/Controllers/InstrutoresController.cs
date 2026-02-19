using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreineMais.Api.Data;
using TreineMais.Api.Models;


namespace TreineMais.Api.Controllers
{
    [ApiController]
    [Route("api/instrutores")]
    public class InstrutoresController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;

        public InstrutoresController(
    AppDbContext context,
    PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        // ======================================================
        // LISTAR TODOS OS INSTRUTORES
        // GET: api/instrutores
        // ======================================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var instrutores = await _context.Users.Where(u => u.Tipo == "INSTRUTOR").ToListAsync();

            return Ok(instrutores);
        }

        // ======================================================
        // BUSCAR INSTRUTOR POR ID
        // GET: api/instrutores/{id}
        // ======================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var instrutor = await _context.Users.FindAsync(id);

            if (instrutor == null || instrutor.Tipo != "INSTRUTOR")
                return NotFound();

            return Ok(instrutor);
        }

        // ======================================================
        // CRIAR NOVO INSTRUTOR
        // POST: api/instrutores
        // ======================================================
        [HttpPost]
        public async Task<IActionResult> Create(User instrutor)
        {
            var usuario = new User
            {
                Nome = instrutor.Nome,
                Email = instrutor.Email,
                Tipo = "INSTRUTOR",
                Senha = _passwordService.HashPassword(instrutor.Senha)
            };

            _context.Users.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(usuario);
        }


        // ======================================================
        // ATUALIZAR INSTRUTOR
        // PUT: api/instrutores/{id}
        // ======================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User instrutor)
        {
            var existente = await _context.Users.FindAsync(id);

            if (existente == null || existente.Tipo != "INSTRUTOR")
                return NotFound();

            if (string.IsNullOrEmpty(instrutor.Nome) || string.IsNullOrEmpty(instrutor.Email))
            {
                return BadRequest("Nome e Email são obrigatórios.");
            }

            existente.Nome = instrutor.Nome;
            existente.Email = instrutor.Email;

            // Atualiza senha apenas se foi informada
            if (!string.IsNullOrEmpty(instrutor.Senha))
            {
                existente.Senha = _passwordService.HashPassword(instrutor.Senha);
            }


            await _context.SaveChangesAsync();
            return Ok(existente);
        }

        // ======================================================
        // EXCLUIR INSTRUTOR + ALUNOS + TREINOS + EXERCÍCIOS
        // DELETE: api/instrutores/{id}
        // ======================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var instrutor = await _context.Users.FindAsync(id);

            if (instrutor == null || instrutor.Tipo != "INSTRUTOR")
                return NotFound();

            // Buscar alunos vinculados ao instrutor
            var alunos = await _context.Alunos
                .Where(a => a.InstrutorId == id)
                .ToListAsync();

            foreach (var aluno in alunos)
            {
                // Buscar treinos do aluno
                var treinos = await _context.Treinos
                    .Where(t => t.AlunoId == aluno.Id)
                    .ToListAsync();

                foreach (var treino in treinos)
                {
                    // Buscar exercícios do treino
                    var exercicios = await _context.Exercicios
                        .Where(e => e.TreinoId == treino.Id)
                        .ToListAsync();

                    foreach (var exercicio in exercicios)
                    {
                        // Excluir conclusões do exercício
                        var conclusoes = await _context.ExercicioConclusoes
                            .Where(c => c.ExercicioId == exercicio.Id)
                            .ToListAsync();

                        _context.ExercicioConclusoes.RemoveRange(conclusoes);
                    }

                    _context.Exercicios.RemoveRange(exercicios);
                }

                _context.Treinos.RemoveRange(treinos);
            }

            _context.Alunos.RemoveRange(alunos);

            // Por fim, excluir o instrutor
            _context.Users.Remove(instrutor);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
