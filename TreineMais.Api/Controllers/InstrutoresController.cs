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

        public InstrutoresController(AppDbContext context)
        {
            _context = context;
        }

        // LISTAR
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var instrutores = await _context.Users.Where(u => u.Tipo == "INSTRUTOR").ToListAsync();

            return Ok(instrutores);
        }

        // CRIAR
        [HttpPost]
        public async Task<IActionResult> Post(User instrutor)
        {
            instrutor.Tipo = "INSTRUTOR";
            _context.Users.Add(instrutor);
            await _context.SaveChangesAsync();

            return Ok(instrutor);
        }

        // EDITAR
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, User instrutor)
        {
            var existente = await _context.Users.FindAsync(id);
            if (existente == null)
                return NotFound();

            existente.Nome = instrutor.Nome;
            existente.Email = instrutor.Email;
            existente.Senha = instrutor.Senha;

            await _context.SaveChangesAsync();
            return Ok(existente);
        }

        // EXCLUIR
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var instrutor = await _context.Users.FindAsync(id);
            if (instrutor == null)
                return NotFound();

            _context.Users.Remove(instrutor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
