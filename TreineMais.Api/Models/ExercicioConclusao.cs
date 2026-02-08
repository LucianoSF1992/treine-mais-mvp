using System;

namespace TreineMais.Api.Models
{
    public class ExercicioConclusao
    {
        public int Id { get; set; }

        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; } = null!;   

        public int ExercicioId { get; set; }
        public Exercicio Exercicio { get; set; } = null!;  

        public DateTime DataExecucao { get; set; }

        public bool Concluido { get; set; }
    }
}
