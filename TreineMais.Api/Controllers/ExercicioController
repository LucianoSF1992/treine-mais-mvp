using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreineMais.Api.Data;
using TreineMais.Api.Models;

namespace TreineMais.Api.Controllers
{
    [ApiController]
    [Route("api/exercicios")]
    public class ExerciciosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExerciciosController(AppDbContext context)
        {
            _context = context;
        }

        // ======================================================
        // LISTAR EXERCÍCIOS POR TREINO
        // GET: api/exercicios/treino/{treinoId}
        // ======================================================
        [HttpGet("treino/{treinoId}")]
        public async Task<IActionResult> GetByTreino(int treinoId)
        {
            var exercicios = await _context.Exercicios
                .Where(e => e.TreinoId == treinoId)
                .ToListAsync();

            return Ok(exercicios);
        }

        // ======================================================
        // BUSCAR EXERCÍCIO POR ID
        // GET: api/exercicios/{id}
        // ======================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var exercicio = await _context.Exercicios.FindAsync(id);

            if (exercicio == null)
                return NotFound();

            return Ok(exercicio);
        }

        // ======================================================
        // CRIAR EXERCÍCIO
        // POST: api/exercicios
        // ======================================================
        [HttpPost]
        public async Task<IActionResult> Create(Exercicio exercicio)
        {
            if (string.IsNullOrEmpty(exercicio.Nome))
                return BadRequest("Nome é obrigatório.");

            if (exercicio.Series <= 0 || exercicio.Repeticoes <= 0)
                return BadRequest("Séries e repetições devem ser maiores que zero.");

            // Verifica se treino existe
            var treinoExiste = await _context.Treinos
                .AnyAsync(t => t.Id == exercicio.TreinoId);

            if (!treinoExiste)
                return BadRequest("Treino não encontrado.");

            _context.Exercicios.Add(exercicio);
            await _context.SaveChangesAsync();

            return Ok(exercicio);
        }

        // ======================================================
        // ATUALIZAR EXERCÍCIO
        // PUT: api/exercicios/{id}
        // ======================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Exercicio exercicio)
        {
            var existente = await _context.Exercicios.FindAsync(id);

            if (existente == null)
                return NotFound();

            if (string.IsNullOrEmpty(exercicio.Nome))
                return BadRequest("Nome é obrigatório.");

            existente.Nome = exercicio.Nome;
            existente.Series = exercicio.Series;
            existente.Repeticoes = exercicio.Repeticoes;

            await _context.SaveChangesAsync();

            return Ok(existente);
        }

        // ======================================================
        // EXCLUIR EXERCÍCIO
        // DELETE: api/exercicios/{id}
        // ======================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exercicio = await _context.Exercicios.FindAsync(id);

            if (exercicio == null)
                return NotFound();

            // Remove conclusões vinculadas
            var conclusoes = await _context.ExercicioConclusoes
                .Where(c => c.ExercicioId == id)
                .ToListAsync();

            _context.ExercicioConclusoes.RemoveRange(conclusoes);
            _context.Exercicios.Remove(exercicio);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}