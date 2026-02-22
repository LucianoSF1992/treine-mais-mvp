namespace TreineMais.Api.DTOs
{
    public class TreinoDto
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public int AlunoId { get; set; }

        public List<ExercicioTreinoDto>? Exercicios { get; set; }
    }
}