namespace TreineMais.Api.DTOs
{
    public class ExercicioTreinoDto
    {
        public int Id { get; set; }

        public int ExercicioId { get; set; }

        public string NomeExercicio { get; set; } = string.Empty;

        public int Series { get; set; }

        public int Repeticoes { get; set; }
    }
}