namespace TreineMais.Api.DTOs
{
    public class CreateTreinoDto
    {
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }

        public int AlunoId { get; set; }
        public string? DiaSemana { get; set; }
    }
}