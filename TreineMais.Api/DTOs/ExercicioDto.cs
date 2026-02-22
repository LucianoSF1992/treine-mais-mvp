namespace TreineMais.Api.Dtos
{
    public class ExercicioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int ExercicioId { get; set; }

        public int Series { get; set; }

        public int Repeticoes { get; set; }
    }
}