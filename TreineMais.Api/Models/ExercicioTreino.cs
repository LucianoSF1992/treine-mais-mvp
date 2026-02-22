namespace TreineMais.Api.Models
{
    public class ExercicioTreino
    {
        public int Id { get; set; }

        public int TreinoId { get; set; }
        public Treino Treino { get; set; } = null!;

        public int ExercicioId { get; set; }
        public Exercicio Exercicio { get; set; } = null!;

        public int Series { get; set; }
        public int Repeticoes { get; set; }
    }
}