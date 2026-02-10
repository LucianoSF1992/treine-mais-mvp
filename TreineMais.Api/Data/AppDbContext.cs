using Microsoft.EntityFrameworkCore;
using TreineMais.Api.Models;

namespace TreineMais.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Treino> Treinos { get; set; }
        public DbSet<Exercicio> Exercicios { get; set; }
        public DbSet<ExercicioConclusao> ExercicioConclusoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExercicioConclusao>()
                .HasIndex(e => new { e.AlunoId, e.ExercicioId })
                .HasDatabaseName("IX_Aluno_Exercicio")
                .IsUnique();
        }
    }
}
