namespace TreineMais.Web.DTOs
{
    public class TreinoDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; } = string.Empty;
        public string? DiaSemana { get; set; } = string.Empty;
        public List<ExercicioDto> Exercicios { get; set; } = new();
    }
}