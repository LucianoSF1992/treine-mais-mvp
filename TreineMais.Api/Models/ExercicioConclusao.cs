using System;

namespace TreineMais.Api.Models
{
    public class ExercicioConclusao
    {
        public int Id { get; set; }

        public int AlunoId { get; set; }   
        public int ExercicioId { get; set; }

        public DateTime DataExecucao { get; set; }
        public bool Concluido { get; set; }
    }
}

