namespace TreineMais.Api.Models
{
    public class TreinoAluno
    {
        public int Id { get; set; }

        public int TreinoId { get; set; }
        public Treino? Treino { get; set; }

        public int AlunoId { get; set; }
        public Aluno? Aluno { get; set; }

        public string? DiaSemana { get; set; }
    }
}
