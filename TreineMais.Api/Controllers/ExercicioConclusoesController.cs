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
            conclusao.DataExecucao = DateTime.Now;
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

    }
}
