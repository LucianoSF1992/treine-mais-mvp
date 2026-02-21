namespace TreineMais.Api.DTOs
{
    public class HistoricoDto
    {
        public string NomeExercicio { get; set; } = "";
        public string GrupoMuscular { get; set; } = "";
        public DateTime DataExecucao { get; set; }
        public bool Concluido { get; set; }
    }
}
