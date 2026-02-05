namespace TreineMais.Api.Models
{
    public class Exercicio
    {
        public int Id { get; set; }
        public int TreinoId { get; set; }
        public string? Nome { get; set; }
        public string? GrupoMuscular { get; set; }
        public int Series { get; set; }
        public int Repeticoes { get; set; }
        public int Descanso { get; set; }
        public string? Observacoes { get; set; }
    }
}
