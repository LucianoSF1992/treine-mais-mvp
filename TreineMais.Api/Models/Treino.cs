namespace TreineMais.Api.Models
{
    public class Treino
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        public int AlunoId { get; set; }

        public int InstrutorId { get; set; }

        public ICollection<ExercicioTreino> ExerciciosTreino { get; set; }
            = new List<ExercicioTreino>();
    }
}