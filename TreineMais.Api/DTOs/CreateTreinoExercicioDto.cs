namespace TreineMais.Api.DTOs
{
    public class CreateTreinoExercicioDto
    {
        public int TreinoId { get; set; }
        public int ExercicioId { get; set; }
        public int Series { get; set; }
        public int Repeticoes { get; set; }
    }
}