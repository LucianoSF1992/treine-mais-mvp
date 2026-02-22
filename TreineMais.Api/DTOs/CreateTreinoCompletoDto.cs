using TreineMais.Api.DTOs;
namespace TreineMais.Api.DTOs
{
    public class CreateTreinoCompletoDto
    {
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }

        public int AlunoId { get; set; }
        public string? DiaSemana { get; set; }

        public List<CreateTreinoExercicioDto> Exercicios { get; set; } = new();
    }

    public class ExercicioDtoCompleto
    {
        public int ExercicioId { get; set; }
        public int Series { get; set; }
        public int Repeticoes { get; set; }
    }
}