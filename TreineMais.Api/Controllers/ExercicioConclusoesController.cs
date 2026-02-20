using Microsoft.AspNetCore.Mvc;
using TreineMais.Api.Data;
using TreineMais.Api.Models;
using Microsoft.EntityFrameworkCore;


namespace TreineMais.Api.Controllers
{
    [ApiController]
    [Route("api/exercicio-conclusoes")]
    public class ExercicioConclusoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExercicioConclusoesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> ConcluirExercicio([FromBody] ExercicioConclusao conclusao)
        {
            // ✅ Valida se exercício existe
            var exercicioExiste = await _context.Exercicios
                .AnyAsync(e => e.Id == conclusao.ExercicioId);

            if (!exercicioExiste)
                return NotFound("Exercício não encontrado.");

            // ✅ Evita duplicidade no mesmo dia
            var hoje = DateTime.UtcNow.Date;

            var jaConcluidoHoje = await _context.ExercicioConclusoes
                .AnyAsync(x =>
                    x.AlunoId == conclusao.AlunoId &&
                    x.ExercicioId == conclusao.ExercicioId &&
                    x.DataExecucao.Date == hoje);

            if (jaConcluidoHoje)
                return BadRequest("Exercício já foi concluído hoje.");

            conclusao.DataExecucao = DateTime.UtcNow;
            conclusao.Concluido = true;

            _context.ExercicioConclusoes.Add(conclusao);
            await _context.SaveChangesAsync();

            return Ok(conclusao);
        }

        [HttpGet("aluno/{alunoId}")]
        public async Task<IActionResult> GetPorAluno(int alunoId)
        {
            var historico = await _context.ExercicioConclusoes
                .Where(x => x.AlunoId == alunoId)
                .OrderByDescending(x => x.DataExecucao)
                .Select(x => new
                {
                    NomeExercicio = x.Exercicio.Nome,
                    GrupoMuscular = x.Exercicio.GrupoMuscular,
                    x.DataExecucao,
                    x.Concluido
                })
                .ToListAsync();

            return Ok(historico);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoverConclusao(int alunoId, int exercicioId)
        {
            var hoje = DateTime.UtcNow.Date;

            var registro = await _context.ExercicioConclusoes
                .FirstOrDefaultAsync(x =>
                    x.AlunoId == alunoId &&
                    x.ExercicioId == exercicioId &&
                    x.DataExecucao.Date == hoje);

            if (registro == null)
                return NotFound("Nenhuma conclusão encontrada hoje.");

            _context.ExercicioConclusoes.Remove(registro);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
