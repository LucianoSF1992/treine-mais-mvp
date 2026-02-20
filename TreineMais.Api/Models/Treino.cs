namespace TreineMais.Api.Models
{
    public class Treino
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        public int InstrutorId { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }

}
