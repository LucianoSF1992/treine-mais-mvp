namespace TreineMais.Web.DTOs
{
    public class ExercicioTreinoDto
    {
        public int ExercicioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string GrupoMuscular { get; set; } = string.Empty;

        public bool Concluido { get; set; }
    }
}