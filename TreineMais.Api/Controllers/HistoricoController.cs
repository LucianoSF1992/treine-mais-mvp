using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreineMais.Api.Data;

namespace TreineMais.Api.Controllers
{
    [ApiController]
    [Route("api/historico")]
    public class HistoricoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HistoricoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{alunoId}")]
        public async Task<IActionResult> GetHistorico(int alunoId)
        {
            var historico = await _context.ExercicioConclusoes
                .Include(e => e.Exercicio)
                .Where(e => e.AlunoId == alunoId)
                .OrderByDescending(e => e.DataExecucao)
                .ToListAsync();

            return Ok(historico);
        }
    }
}
