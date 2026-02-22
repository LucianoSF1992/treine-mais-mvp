using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreineMais.Api.Data;
using TreineMais.Api.DTOs;
using TreineMais.Api.Models;

namespace TreineMais.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TreinosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TreinosController(AppDbContext context)
        {
            _context = context;
        }

        // =====================================================
        // 🔐 HELPER — PEGAR ID DO USUÁRIO LOGADO (SEGURO)
        // =====================================================
        private int GetUserId()
        {
            var claim = User.FindFirst("UserId");

            if (claim == null)
                throw new UnauthorizedAccessException("Usuário não autenticado");

            return int.Parse(claim.Value);
        }

        // =====================================================
        // 🥇 LISTAR TREINOS DO ALUNO LOGADO
        // =====================================================
        [HttpGet("meus")]
        public async Task<ActionResult<List<TreinoDto>>> GetMeusTreinos()
        {
            var alunoId = GetUserId();

            var treinos = await _context.TreinoAlunos
                .Where(ta => ta.AlunoId == alunoId && ta.Treino != null)
                .Include(ta => ta.Treino!)
                    .ThenInclude(t => t.ExerciciosTreino)
                .Select(ta => new TreinoDto
                {
                    Id = ta.Treino!.Id,
                    Nome = ta.Treino!.Nome,
                    Descricao = ta.Treino!.Descricao,
                    Exercicios = ta.Treino!.ExerciciosTreino.Select(et => new ExercicioTreinoDto
                    {
                        Id = et.Id,
                        ExercicioId = et.ExercicioId,
                        Series = et.Series,
                        Repeticoes = et.Repeticoes
                    }).ToList()
                })
                .ToListAsync();

            return treinos;
        }

        // =====================================================
        // 🥈 LISTAR TREINOS DO INSTRUTOR
        // =====================================================
        [HttpGet("instrutor")]
        public async Task<ActionResult<List<TreinoDto>>> GetTreinosInstrutor()
        {
            var instrutorId = GetUserId();

            var treinos = await _context.Treinos
                .Where(t => t.InstrutorId == instrutorId)
                .Include(t => t.ExerciciosTreino)
                    .ThenInclude(et => et.Exercicio)
                .Select(t => new TreinoDto
                {
                    Id = t.Id,
                    Nome = t.Nome,
                    Descricao = t.Descricao,
                    Exercicios = t.ExerciciosTreino.Select(et => new ExercicioTreinoDto
                    {
                        Id = et.Id,
                        ExercicioId = et.ExercicioId,
                        Series = et.Series,
                        Repeticoes = et.Repeticoes
                    }).ToList()
                })
                .ToListAsync();

            return treinos;
        }

        // =====================================================
        // 🥉 BUSCAR TREINO POR ID
        // =====================================================
        [HttpGet("{id}")]
        public async Task<ActionResult<TreinoDto>> GetTreino(int id)
        {
            var treino = await _context.Treinos
                .Include(t => t.ExerciciosTreino)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (treino == null)
                return NotFound();

            return new TreinoDto
            {
                Id = treino.Id,
                Nome = treino.Nome,
                Descricao = treino.Descricao,
                Exercicios = treino.ExerciciosTreino.Select(et => new ExercicioTreinoDto
                {
                    Id = et.Id,
                    ExercicioId = et.ExercicioId,
                    Series = et.Series,
                    Repeticoes = et.Repeticoes
                }).ToList()
            };
        }

        // =====================================================
        // 🚀 CRIAR TREINO COMPLETO
        // =====================================================
        [HttpPost("completo")]
        public async Task<IActionResult> CreateCompleto(CreateTreinoCompletoDto dto)
        {
            var instrutorId = GetUserId();

            if (dto.Exercicios == null || !dto.Exercicios.Any())
                return BadRequest("Treino precisa ter exercícios.");

            var treino = new Treino
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                InstrutorId = instrutorId,
                AlunoId = dto.AlunoId
            };

            _context.Treinos.Add(treino);
            await _context.SaveChangesAsync();

            // vínculo aluno
            var treinoAluno = new TreinoAluno
            {
                TreinoId = treino.Id,
                AlunoId = dto.AlunoId,
                DiaSemana = dto.DiaSemana
            };

            _context.TreinoAlunos.Add(treinoAluno);

            // exercícios
            foreach (var ex in dto.Exercicios)
            {
                _context.ExercicioTreinos.Add(new ExercicioTreino
                {
                    TreinoId = treino.Id,
                    ExercicioId = ex.ExercicioId,
                    Series = ex.Series,
                    Repeticoes = ex.Repeticoes
                });
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        // =====================================================
        // ✏ EDITAR TREINO
        // =====================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTreino(int id, CreateTreinoCompletoDto dto)
        {
            var treino = await _context.Treinos
                .Include(t => t.ExerciciosTreino)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (treino == null)
                return NotFound();

            treino.Nome = dto.Nome;
            treino.Descricao = dto.Descricao;

            // remove antigos
            _context.ExercicioTreinos.RemoveRange(treino.ExerciciosTreino);

            // adiciona novos
            foreach (var ex in dto.Exercicios)
            {
                _context.ExercicioTreinos.Add(new ExercicioTreino
                {
                    TreinoId = treino.Id,
                    ExercicioId = ex.ExercicioId,
                    Series = ex.Series,
                    Repeticoes = ex.Repeticoes
                });
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // =====================================================
        // 🗑 EXCLUIR TREINO
        // =====================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreino(int id)
        {
            var treino = await _context.Treinos
                .Include(t => t.ExerciciosTreino)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (treino == null)
                return NotFound();

            _context.ExercicioTreinos.RemoveRange(treino.ExerciciosTreino);
            _context.Treinos.Remove(treino);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}