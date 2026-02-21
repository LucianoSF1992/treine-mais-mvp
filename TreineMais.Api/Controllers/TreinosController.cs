using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreineMais.Api.Data;
using TreineMais.Api.Models;

namespace TreineMais.Api.Controllers
{
    [ApiController]
    [Route("api/treinos")]
    public class TreinosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TreinosController(AppDbContext context)
        {
            _context = context;
        }
        // POST: api/treinos
        [HttpPost]
        public async Task<IActionResult> CriarTreino(Treino treino)
        {
            if (string.IsNullOrEmpty(treino.Nome))
                return BadRequest("Nome do treino é obrigatório");

            _context.Treinos.Add(treino);
            await _context.SaveChangesAsync();

            return Ok(treino);
        }
        // GET: api/treinos/instrutor/5
        [HttpGet("instrutor/{instrutorId}")]
        public async Task<IActionResult> ListarPorInstrutor(int instrutorId)
        {
            var treinos = await _context.Treinos
                .Where(t => t.InstrutorId == instrutorId)
                .ToListAsync();

            return Ok(treinos);
        }
        // POST: api/treinos/vincular-aluno
        [HttpPost("vincular-aluno")]
        public async Task<IActionResult> VincularAluno(int treinoId, int alunoId)
        {
            var treinoExiste = await _context.Treinos.AnyAsync(t => t.Id == treinoId);
            var alunoExiste = await _context.Alunos.AnyAsync(a => a.Id == alunoId);

            if (!treinoExiste || !alunoExiste)
                return NotFound("Treino ou aluno não encontrado");

            var vinculo = new TreinoAluno
            {
                TreinoId = treinoId,
                AlunoId = alunoId
            };

            _context.TreinoAlunos.Add(vinculo);
            await _context.SaveChangesAsync();

            return Ok(vinculo);
        }
        // GET: api/treinos/aluno/3
        [HttpGet("aluno/{alunoId}")]
        public async Task<IActionResult> ListarTreinosAluno(int alunoId)
        {
            var treinos = await _context.TreinoAlunos
                .Where(ta => ta.AlunoId == alunoId)
                .Include(ta => ta.Treino)
                .Select(ta => ta.Treino)
                .ToListAsync();

            return Ok(treinos);
        }

        [HttpGet("treino/{treinoId}/aluno/{alunoId}")]
        public async Task<IActionResult> GetExerciciosTreinoAluno(int treinoId, int alunoId)
        {
            var exercicios = await _context.Exercicios
                .Where(e => e.TreinoId == treinoId)
                .Select(e => new ExercicioTreinoDto
                {
                    ExercicioId = e.Id,
                    Nome = e.Nome,
                    GrupoMuscular = e.GrupoMuscular,

                    Concluido = _context.ExercicioConclusoes
                        .Any(c =>
                            c.ExercicioId == e.Id &&
                            c.AlunoId == alunoId &&
                            c.Concluido)
                })
                .ToListAsync();

            return Ok(exercicios);
        }
    }
}